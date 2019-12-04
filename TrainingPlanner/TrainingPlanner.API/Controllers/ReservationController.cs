using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Reservation;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation([FromBody] ReservationDTO reservation)
        {
            try
            {
                var res = await _reservationService.CreateReservation(reservation);
                return Ok(res);
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

        [HttpDelete]
        public async Task<ActionResult> DeleteReservation([FromQuery] int trainingId, [FromQuery] string userId)
        {
            try
            {
                await _reservationService.DeleteReservation(trainingId, userId);
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
