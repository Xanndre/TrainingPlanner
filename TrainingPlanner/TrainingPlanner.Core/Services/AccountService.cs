﻿using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Account;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Options;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace TrainingPlanner.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly FacebookLoginOptions _fbLoginOptions;
        private readonly EmailOptions _emailOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly INotificationRepository _notificationRepository;

        public AccountService(
            IOptions<JwtOptions> jwtOptions,
            IOptions<FacebookLoginOptions> fbLoginOptions,
            IOptions<EmailOptions> emailOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IHttpClientFactory clientFactory,
            IMapper mapper,
            IEmailService emailService,
            INotificationRepository notificationRepository
            )
        {
            _jwtOptions = jwtOptions.Value;
            _fbLoginOptions = fbLoginOptions.Value;
            _emailOptions = emailOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _clientFactory = clientFactory;
            _mapper = mapper;
            _emailService = emailService;
            _notificationRepository = notificationRepository;
        }

        public async Task<LoginResultDTO> Login(LoginDTO loginDTO)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == loginDTO.Email);

            if (! await _userManager.IsEmailConfirmedAsync(user) && !_userManager.GetLoginsAsync(user).Result.Any())
            {
                throw new ApplicationException(DictionaryResources.EmailNotConfirmed);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidLoginAttempt);
            }

            var loginResult = new LoginResultDTO
            {
                Token = GenerateJwtToken(user),
                Id = user.Id
            };

            return loginResult;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            var user = _mapper.Map<ApplicationUser>(registerDTO);
            if (await _userManager.FindByEmailAsync(user.Email) != null)
                throw new ArgumentException(DictionaryResources.AccountExists);
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidRegistrationAttempt);
            }

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = $"{_emailOptions.ConfirmUrl}?id={user.Id}&token={WebUtility.UrlEncode(emailToken)}";
            var message = "Hello " + user.FirstName + DictionaryResources.Message + link + DictionaryResources.Thanks;
            
            var emailResult = await _emailService.SendEmail(registerDTO.Email, DictionaryResources.EmailConfirmation, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }
        }

        public async Task SendEmailAgain(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = $"{_emailOptions.ConfirmUrl}?id={user.Id}&token={WebUtility.UrlEncode(emailToken)}";
            var message = "Hello " + user.FirstName + DictionaryResources.Message + link + DictionaryResources.Thanks;

            var emailResult = await _emailService.SendEmail(user.Email, DictionaryResources.EmailConfirmation, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }
        }

        public async Task<string> ConfirmEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if(user == null)
            {
                return _emailOptions.UserErrorUrl;
            }

            var errorUrl = _emailOptions.ConfirmErrorUrl + user.Id;
            var successUrl = _emailOptions.ConfirmSuccessUrl + user.Id;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return errorUrl;
            }

            await CreateNotification(id);

            return successUrl;
        }

        public async Task ChangePassword(ChangePasswordDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if(user == null)
            {
                throw new ApplicationException(DictionaryResources.NoUser);
            }
            if(user.Email != dto.Email)
            {
                throw new ApplicationException(DictionaryResources.WrongEmail);
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidChangePasswordAttempt);
            }
        }

        public async Task SendResetToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await SendToken(user);
        }

        public async Task SendResetTokenAgain(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await SendToken(user);
        }

        public async Task ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                throw new ApplicationException(DictionaryResources.NoUser);
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidResetPasswordAttempt);
            }

        }


        public async Task<LoginResultDTO> ExternalLogin(ExternalLoginDTO loginDTO)
        {
            if (loginDTO.LoginProvider == DictionaryResources.Facebook)
            {
                await ValidateFacebookToken(loginDTO.Token);
            }
            else
            {
                await ValidateGoogleToken(loginDTO.Token);
            }

            var user = _userManager.Users
                .SingleOrDefault(u => u.Email == loginDTO.Email);

            if (user == null)
            {
                user = await ExternalRegister(loginDTO);
            }

            var loggedUser = await _userRepository.GetUserLogin(loginDTO.LoginProvider, user.Id);
            if (loggedUser == null)
            {
                await AddLogin(loginDTO, user);
            }

            var result = await _signInManager
                .ExternalLoginSignInAsync(loginDTO.LoginProvider, loginDTO.ProviderKey, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidLoginAttempt);
            }

            var loginResult = new LoginResultDTO
            {
                Token = GenerateJwtToken(user),
                Id = user.Id
            };
            return loginResult;
        }

        private async Task SendToken(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ApplicationException(DictionaryResources.NoUser);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var link = $"{_emailOptions.ResetUrl}?id={user.Id}&token={WebUtility.UrlEncode(token)}";
            var message = "Hello " + user.FirstName + DictionaryResources.PasswordResetMessage + link + DictionaryResources.PasswordResetThanks;

            var emailResult = await _emailService.SendEmail(user.Email, DictionaryResources.PasswordReset, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }
        }

        private async Task<ApplicationUser> ExternalRegister(ExternalLoginDTO loginDTO)
        {
            var user = _mapper.Map<ApplicationUser>(loginDTO);
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidRegistrationAttempt);
            }

            await AddLogin(loginDTO, user);
            await CreateNotification(user.Id);
            return user;
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOptions.JwtExpireDays));

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task AddLogin(ExternalLoginDTO loginDTO, ApplicationUser user)
        {
            var userLoginInfo = new UserLoginInfo(loginDTO.LoginProvider, loginDTO.ProviderKey, null);
            var externalLoginResult = await _userManager.AddLoginAsync(user, userLoginInfo);

            if (!externalLoginResult.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidRegistrationAttempt);
            }
        }

        private async Task ValidateFacebookToken(string token)
        {
            var client = _clientFactory.CreateClient();
            var uri = string
                .Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}", token, _fbLoginOptions.AppId, _fbLoginOptions.AppSecret);
            var verifyTokenResult = await client.GetStringAsync(uri);

            var isValid = JObject.Parse(verifyTokenResult)["data"].Value<bool>("is_valid");
            if (!isValid)
            {
                throw new AuthenticationException(DictionaryResources.InvalidToken);
            }
        }

        private async Task ValidateGoogleToken(string token)
        {
            await GoogleJsonWebSignature.ValidateAsync(token);
        }

        private async Task CreateNotification(string id)
        {
            var notification = new Notification()
            {
                UserId = id,
                CardAlmostExpired = true,
                CardExpired = true,
                IncomingTraining = true,
                SignUpConfirmed = true,
                SignOutConfirmed = true,
                TrainingDeleted = true,
                ReserveListToList = true,
                ListToReserveList = true,
                ReserveListSignUpConfirmed = true,
                ReserveListSignOutConfirmed = true
            };

            await _notificationRepository.CreateNotification(notification);
        }
    }
}
