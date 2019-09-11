using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Options;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace TrainingPlanner.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ConfigurationOptions _options;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountService(
            IOptions<ConfigurationOptions> options,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper
            )
        {
            _options = options.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public string GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_options.JwtExpireDays));

            var token = new JwtSecurityToken
            (
                _options.JwtIssuer,
                _options.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                Token = GenerateJwtToken(loginDTO.Email, user),
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
    }
}
