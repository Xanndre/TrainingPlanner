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
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDTO>>> GetAllClubs()
        {
            try
            {
                return Ok(await _clubService.GetAllClubs());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDTO>> GetClub(int id)
        {
            try
            {
                var club = await _clubService.GetClub(id);
                return Ok(club);
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
        public async Task<ActionResult<ClubDTO>> UpdateClub([FromBody] ClubDTO club)
        {
            try
            {
                var returnedClub = await _clubService.UpdateClub(club);
                return Ok(returnedClub);
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
        public async Task<ActionResult<ClubCreateDTO>> CreateClub([FromBody] ClubCreateDTO club)
        {
            try
            {
                var returnedClub = await _clubService.CreateClub(club);
                return Ok(returnedClub);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClub(int id)
        {
            try
            {
                await _clubService.DeleteClub(id);
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
