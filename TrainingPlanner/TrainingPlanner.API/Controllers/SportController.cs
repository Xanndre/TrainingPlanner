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
    public class SportController : ControllerBase
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetAllSports()
        {
            try
            {
                return Ok(await _sportService.GetAllSports());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSportsByNames([FromQuery] string sportNames)
        {
            try
            {
                return Ok(await _sportService.GetSportsByNames(sportNames));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SportDTO>> GetSport(int id)
        {
            try
            {
                var sport = await _sportService.GetSport(id);
                return Ok(sport);
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
        public async Task<ActionResult<SportDTO>> UpdateSport([FromBody] SportDTO sport)
        {
            try
            {
                var returnedSport = await _sportService.UpdateSport(sport);
                return Ok(returnedSport);
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
        public async Task<ActionResult<SportDTO>> CreateSport([FromBody] SportDTO sport)
        {
            try
            {
                var returnedSport = await _sportService.CreateSport(sport);
                return Ok(returnedSport);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSport(int id)
        {
            try
            {
                await _sportService.DeleteSport(id);
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
    }
}
