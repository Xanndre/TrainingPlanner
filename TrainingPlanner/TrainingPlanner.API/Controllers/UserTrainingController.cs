using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.UserStuff.UserTraining;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTrainingController : ControllerBase
    {
        private readonly IUserTrainingService _userTrainingService;

        public UserTrainingController(IUserTrainingService userTrainingService)
        {
            _userTrainingService = userTrainingService;
        }

        [HttpPut]
        public async Task<ActionResult<UserTrainingDTO>> UpdateUserTraining([FromBody] UserTrainingDTO training)
        {
            try
            {
                var returnedTraining = await _userTrainingService.UpdateUserTraining(training);
                return Ok(returnedTraining);
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

        [HttpPost]
        public async Task<ActionResult<UserTrainingCreateDTO>> CreateUserTraining([FromBody] UserTrainingCreateDTO training)
        {
            try
            {
                var returnedTraining = await _userTrainingService.CreateUserTraining(training);
                return Ok(returnedTraining);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserTraining(int id)
        {
            try
            {
                await _userTrainingService.DeleteUserTraining(id);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTrainingDTO>> GetUserTraining(int id)
        {
            try
            {
                var training = await _userTrainingService.GetUserTraining(id);
                return Ok(training);
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

        [HttpGet]
        public async Task<ActionResult<PagedUserTrainingsDTO>> GetUserTrainings(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _userTrainingService.GetAllUserTrainings(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
