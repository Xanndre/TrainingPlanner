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
        public async Task<ActionResult<ClubUpdateDTO>> UpdateClub([FromBody] ClubUpdateDTO club)
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

        [HttpGet]
        public async Task<ActionResult<PagedClubsDTO>> GetAllClubs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3,
            [FromQuery] string userId = null)
        {
            try
            {
                var result = await _clubService.GetAllClubs(pageNumber, pageSize, userId);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("favourites")]
        public async Task<ActionResult<PagedClubsDTO>> GetFavouriteClubs(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _clubService.GetFavouriteClubs(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("user")]
        public async Task<ActionResult<PagedClubsDTO>> GetUserClubs(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                return Ok(await _clubService.GetUserClubs(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("user/{userId}/quantity")]
        public async Task<ActionResult<int>> GetClubQuantity(string userId)
        {
            try
            {
                var quantity = await _clubService.GetClubQuantity(userId);
                return Ok(quantity);
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

        [HttpGet("user/{userId}/ids")]
        public async Task<ActionResult<IEnumerable<int>>> GetClubIds(string userId)
        {
            try
            {
                var ids = await _clubService.GetClubIds(userId);
                return Ok(ids);
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
