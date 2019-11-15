using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.BodyMeasurement;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BodyMeasurementController : ControllerBase
    {
        private readonly IBodyMeasurementService _bodyMeasurementService;

        public BodyMeasurementController(IBodyMeasurementService bodyMeasurementService)
        {
            _bodyMeasurementService = bodyMeasurementService;
        }

        [HttpPut]
        public async Task<ActionResult<BodyMeasurementDTO>> UpdateBodyMeasurement([FromBody] BodyMeasurementDTO measurement)
        {
            try
            {
                var returnedMeasurement = await _bodyMeasurementService.UpdateBodyMeasurement(measurement);
                return Ok(returnedMeasurement);
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
        public async Task<ActionResult<BodyMeasurementCreateDTO>> CreateBodyMeasurement([FromBody] BodyMeasurementCreateDTO measurement)
        {
            try
            {
                var returnedMeasurement = await _bodyMeasurementService.CreateBodyMeasurement(measurement);
                return Ok(returnedMeasurement);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBodyMeasurement(int id)
        {
            try
            {
                await _bodyMeasurementService.DeleteBodyMeasurement(id);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<BodyMeasurementDTO>> GetBodyMeasurement(int id)
        {
            try
            {
                var measurement = await _bodyMeasurementService.GetBodyMeasurement(id);
                return Ok(measurement);
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
        public async Task<ActionResult<PagedBodyMeasurementsDTO>> GetBodyMeasurements(
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _bodyMeasurementService.GetAllBodyMeasurements(pageNumber, pageSize, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
