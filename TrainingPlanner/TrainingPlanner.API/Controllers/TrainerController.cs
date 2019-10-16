using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> GetAllTrainers()
        {
            try
            {
                return Ok(await _trainerService.GetAllTrainers());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainer(int id)
        {
            try
            {
                var trainer = await _trainerService.GetTrainer(id);
                return Ok(trainer);
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
        public async Task<ActionResult<TrainerUpdateDTO>> UpdateTrainer([FromBody] TrainerUpdateDTO trainer)
        {
            try
            {
                var returnedTrainer = await _trainerService.UpdateTrainer(trainer);
                return Ok(returnedTrainer);
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
        public async Task<ActionResult<TrainerCreateDTO>> CreateTrainer([FromBody] TrainerCreateDTO trainer)
        {
            try
            {
                var returnedTrainer = await _trainerService.CreateTrainer(trainer);
                return Ok(returnedTrainer);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            try
            {
                await _trainerService.DeleteTrainer(id);
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
        public async Task<ActionResult<TrainerDTO>> GetTrainerByUser(string userId)
        {
            try
            {
                var trainer = await _trainerService.GetTrainerByUser(userId);
                return Ok(trainer);
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
    }
}
