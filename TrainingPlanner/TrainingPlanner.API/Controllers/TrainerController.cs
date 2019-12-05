using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.Trainer;
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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainer(int id, [FromQuery] bool isIncrementingViewCounter = false)
        {
            try
            {
                var trainer = await _trainerService.GetTrainer(id, isIncrementingViewCounter);
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedTrainersDTO>> GetAllTrainers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3,
            [FromQuery] string userId = null)
        {
            try
            {
                var result = await _trainerService.GetAllTrainers(pageNumber, pageSize, userId);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("favourites")]
        public async Task<ActionResult<PagedTrainersDTO>> GetFavouriteTrainers(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _trainerService.GetFavouriteTrainers(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

    }
}
