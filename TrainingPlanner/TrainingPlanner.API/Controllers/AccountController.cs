using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Account;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var result = await _accountService.Login(loginDTO);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                await _accountService.Register(registerDTO);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Login/External")]
        public async Task<ActionResult> ExternalLogin([FromBody] ExternalLoginDTO loginDTO)
        {
            try
            {
                var result = await _accountService.ExternalLogin(loginDTO);

                return Ok(result);
            }
            catch (InvalidJwtException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (AuthenticationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string id, [FromQuery] string token)
        {
            try
            {
                var result = await _accountService.ConfirmEmail(id, token);
                return Redirect(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("send/{id}")]
        public async Task<ActionResult> SendEmailAgain(string id)
        {
            try
            {
                await _accountService.SendEmailAgain(id);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpPost("change_password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                await _accountService.ChangePassword(dto);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("generate_reset_token")]
        public async Task<ActionResult> SendResetToken([FromQuery] string email)
        {
            try
            {
                await _accountService.SendResetToken(email);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
