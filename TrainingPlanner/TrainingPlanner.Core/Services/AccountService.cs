using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Account;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;

        public AccountService(
            IOptions<JwtOptions> jwtOptions,
            IOptions<FacebookLoginOptions> fbLoginOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IHttpClientFactory clientFactory,
            IMapper mapper
            )
        {
            _jwtOptions = jwtOptions.Value;
            _fbLoginOptions = fbLoginOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _clientFactory = clientFactory;
            _mapper = mapper;
        }

        public async Task<LoginResultDTO> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidLoginAttempt);
            }

            var user = _userManager.Users.SingleOrDefault(u => u.Email == loginDTO.Email);

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

        private async Task<ApplicationUser> ExternalRegister(ExternalLoginDTO loginDTO)
        {
            var user = _mapper.Map<ApplicationUser>(loginDTO);
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new ApplicationException(DictionaryResources.InvalidRegistrationAttempt);
            }

            await AddLogin(loginDTO, user);
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
    }
}
