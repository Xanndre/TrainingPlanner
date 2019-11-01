using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Favourite;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [HttpPost("club")]
        public async Task<ActionResult<FavouriteClubDTO>> CreateFavouriteClub([FromBody] FavouriteClubDTO favourite)
        {
            try
            {
                var fav = await _favouriteService.CreateFavouriteClub(favourite);
                return Ok(fav);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("club/{clubId}")]
        public async Task<ActionResult> DeleteFavouriteClub(int clubId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _favouriteService.DeleteFavouriteClub(clubId, userId);
                return Ok();
            }
            catch (ArgumentNullException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost("trainer")]
        public async Task<ActionResult<FavouriteTrainerDTO>> CreateFavouriteTrainer([FromBody] FavouriteTrainerDTO favourite)
        {
            try
            {
                var fav = await _favouriteService.CreateFavouriteTrainer(favourite);
                return Ok(fav);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("trainer/{trainerId}")]
        public async Task<ActionResult> DeleteFavouriteTrainer(int trainerId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _favouriteService.DeleteFavouriteTrainer(trainerId, userId);
                return Ok();
            }
            catch (ArgumentNullException exception)
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
