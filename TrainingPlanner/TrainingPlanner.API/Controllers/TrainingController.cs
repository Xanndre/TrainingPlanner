﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet("club/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetClubTrainings(
            [FromQuery] TrainingFilterData filterData, 
            int id)
        {
            try
            {
                return Ok(await _trainingService.GetClubTrainings(id, filterData));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("trainer/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainerTrainings(
            [FromQuery] TrainingFilterData filterData, 
            int id)
        {
            try
            {
                return Ok(await _trainingService.GetTrainerTrainings(id, filterData));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> GetTraining(int id)
        {
            try
            {
                var training = await _trainingService.GetTraining(id);
                return Ok(training);
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
        public async Task<ActionResult<TrainingUpdateDTO>> UpdateTraining([FromBody] TrainingUpdateDTO training)
        {
            try
            {
                var returnedTraining = await _trainingService.UpdateTraining(training);
                return Ok(returnedTraining);
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
        public async Task<ActionResult<TrainingCreateDTO>> CreateTraining([FromBody] TrainingCreateDTO training)
        {
            try
            {
                var returnedTraining = await _trainingService.CreateTraining(training);
                return Ok(returnedTraining);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTraining(int id)
        {
            try
            {
                await _trainingService.DeleteTraining(id);
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
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetReservedTrainings(
            [FromQuery] TrainingFilterData filterData, 
            string userId)
        {
            try
            {
                return Ok(await _trainingService.GetReservedTrainings(userId, filterData));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost("range")]
        public async Task<ActionResult<IEnumerable<TrainingCreateDTO>>> CreateTrainingRange(
                        [FromBody] IEnumerable<TrainingCreateDTO> trainings)
        {
            try
            {
                var returnedTrainings = await _trainingService.CreateTrainingRange(trainings);
                return Ok(returnedTrainings);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
