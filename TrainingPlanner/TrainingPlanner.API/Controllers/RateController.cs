using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubRate;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("club")]
        public async Task<ActionResult<ClubRateDTO>> GetClubRate([FromQuery] string userId, [FromQuery] int clubId)
        {
            try
            {
                var rate = await _rateService.GetClubRate(userId, clubId);
                return Ok(rate);
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

        [HttpPut("club")]
        public async Task<ActionResult<ClubRateDTO>> UpdateClubRate([FromBody] ClubRateDTO rate)
        {
            try
            {
                var returnedRate = await _rateService.UpdateClubRate(rate);
                return Ok(returnedRate);
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

        [HttpPost("club")]
        public async Task<ActionResult<ClubRateCreateDTO>> CreateClubRate([FromBody] ClubRateCreateDTO rate)
        {
            try
            {
                var returnedRate = await _rateService.CreateClubRate(rate);
                return Ok(returnedRate);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("club/{id}")]
        public async Task<ActionResult> DeleteClubRate(int id)
        {
            try
            {
                await _rateService.DeleteClubRate(id);
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

        [AllowAnonymous]
        [HttpGet("club/all")]
        public async Task<ActionResult<PagedClubRatesDTO>> GetClubRates(
            [FromQuery] int clubId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _rateService.GetAllClubRates(pageNumber, pageSize, clubId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("trainer")]
        public async Task<ActionResult<TrainerRateDTO>> GetTrainerRate([FromQuery] string userId, [FromQuery] int trainerId)
        {
            try
            {
                var rate = await _rateService.GetTrainerRate(userId, trainerId);
                return Ok(rate);
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

        [HttpPut("trainer")]
        public async Task<ActionResult<TrainerRateDTO>> UpdateTrainerRate([FromBody] TrainerRateDTO rate)
        {
            try
            {
                var returnedRate = await _rateService.UpdateTrainerRate(rate);
                return Ok(returnedRate);
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

        [HttpPost("trainer")]
        public async Task<ActionResult<TrainerRateCreateDTO>> CreateTrainerRate([FromBody] TrainerRateCreateDTO rate)
        {
            try
            {
                var returnedRate = await _rateService.CreateTrainerRate(rate);
                return Ok(returnedRate);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("trainer/{id}")]
        public async Task<ActionResult> DeleteTrainerRate(int id)
        {
            try
            {
                await _rateService.DeleteTrainerRate(id);
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

        [AllowAnonymous]
        [HttpGet("trainer/all")]
        public async Task<ActionResult<PagedTrainerRatesDTO>> GetTrainerRates(
            [FromQuery] int trainerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _rateService.GetAllTrainerRates(pageNumber, pageSize, trainerId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
