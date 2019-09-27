using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs;
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

    }
}
