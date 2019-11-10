using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("club/{id}")]
        public async Task<ActionResult<ClubCardDTO>> GetClubCard(int id)
        {
            try
            {
                var card = await _cardService.GetClubCard(id);
                return Ok(card);
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
        public async Task<ActionResult<ClubCardUpdateDTO>> UpdateClubCard([FromBody] ClubCardUpdateDTO card, [FromQuery] bool isDeactivating = false)
        {
            try
            {
                var returnedCard = await _cardService.UpdateClubCard(card, isDeactivating);
                return Ok(returnedCard);
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
        public async Task<ActionResult<ClubCardCreateDTO>> CreateClubCard([FromBody] ClubCardCreateDTO card)
        {
            try
            {
                var returnedCard = await _cardService.CreateClubCard(card);
                return Ok(returnedCard);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("club/{id}")]
        public async Task<ActionResult> DeleteClubCard(int id)
        {
            try
            {
                await _cardService.DeleteClubCard(id);
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

        [HttpGet("club/user")]
        public async Task<ActionResult<PagedClubCardsDTO>> GetUserClubCards(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetUserClubCards(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("club/club")]
        public async Task<ActionResult<PagedClubCardsDTO>> GetClubClubCards(
            [FromQuery] int clubId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetClubClubCards(pageNumber, pageSize, clubId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("club")]
        public async Task<ActionResult<PagedClubCardsDTO>> GetClubCards(
            [FromQuery] int clubId,
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetClubCards(pageNumber, pageSize, userId, clubId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("trainer/{id}")]
        public async Task<ActionResult<TrainerCardDTO>> GetTrainerCard(int id)
        {
            try
            {
                var card = await _cardService.GetTrainerCard(id);
                return Ok(card);
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
        public async Task<ActionResult<TrainerCardUpdateDTO>> UpdateTrainerCard([FromBody] TrainerCardUpdateDTO card, [FromQuery] bool isDeactivating = false)
        {
            try
            {
                var returnedCard = await _cardService.UpdateTrainerCard(card, isDeactivating);
                return Ok(returnedCard);
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
        public async Task<ActionResult<TrainerCardCreateDTO>> CreateTrainerCard([FromBody] TrainerCardCreateDTO card)
        {
            try
            {
                var returnedCard = await _cardService.CreateTrainerCard(card);
                return Ok(returnedCard);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("trainer/{id}")]
        public async Task<ActionResult> DeleteTrainerCard(int id)
        {
            try
            {
                await _cardService.DeleteTrainerCard(id);
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

        [HttpGet("trainer/user")]
        public async Task<ActionResult<PagedTrainerCardsDTO>> GetUserTrainerCards(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetUserTrainerCards(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("trainer/trainer")]
        public async Task<ActionResult<PagedTrainerCardsDTO>> GetTrainerTrainerCards(
            [FromQuery] int trainerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetTrainerTrainerCards(pageNumber, pageSize, trainerId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("trainer")]
        public async Task<ActionResult<PagedTrainerCardsDTO>> GetTrainerCards(
            [FromQuery] int trainerId,
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _cardService.GetTrainerCards(pageNumber, pageSize, userId, trainerId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
