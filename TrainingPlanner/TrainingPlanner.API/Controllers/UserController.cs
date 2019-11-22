using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<PagedUsersDTO> GetAllUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = _userService.GetAllUsers(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("signed")]
        public async Task<ActionResult<PagedTrainersDTO>> GetSignedUpUsers(
            [FromQuery] int trainingId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _userService.GetSignedUpUsers(pageNumber, pageSize, trainingId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                return Ok(user);
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserDTO user, [FromQuery] bool isPartner = false)
        {
            try
            {
                var appUser = await _userService.UpdateUser(user, isPartner);
                return Ok(appUser);
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("locations")]
        public async Task<ActionResult<string>> GetLocations()
        {
            try
            {
                var locations = await _userService.GetLocations();
                return Ok(locations);
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("partners/{id}")]
        public async Task<ActionResult<PagedPartnersDTO>> GetAllPartners(string id,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                var result = await _userService.GetAllPartners(pageNumber, pageSize, id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
