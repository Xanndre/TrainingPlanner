using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.UserStuff.UserCalendarTraining;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCalendarTrainingController : ControllerBase
    {
        private readonly IUserCalendarTrainingService _trainingService;

        public UserCalendarTrainingController(IUserCalendarTrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserCalendarTrainingDTO>> GetUserCalendarTraining(int id)
        {
            try
            {
                var training = await _trainingService.GetUserCalendarTraining(id);
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

        [HttpPut]
        public async Task<ActionResult<UserCalendarTrainingUpdateDTO>> UpdateUserCalendarTraining([FromBody] UserCalendarTrainingUpdateDTO training)
        {
            try
            {
                var returnedTraining = await _trainingService.UpdateUserCalendarTraining(training);
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
        public async Task<ActionResult<UserCalendarTrainingCreateDTO>> CreateUserCalendarTraining([FromBody] UserCalendarTrainingCreateDTO training)
        {
            try
            {
                var returnedTraining = await _trainingService.CreateUserCalendarTraining(training);
                return Ok(returnedTraining);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserCalendarTraining(int id)
        {
            try
            {
                await _trainingService.DeleteUserCalendarTraining(id);
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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserCalendarTrainingDTO>>> GetUserCalendarTrainings(string userId)
        {
            try
            {
                return Ok(await _trainingService.GetUserCalendarTrainings(userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost("range")]
        public async Task<ActionResult<IEnumerable<UserCalendarTrainingCreateDTO>>> CreateUserCalendarTrainingRange(
                        [FromBody] IEnumerable<UserCalendarTrainingCreateDTO> trainings)
        {
            try
            {
                var returnedTrainings = await _trainingService.CreateUserCalendarTrainingRange(trainings);
                return Ok(returnedTrainings);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
