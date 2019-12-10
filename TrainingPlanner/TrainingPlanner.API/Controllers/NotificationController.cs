using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Notification;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPut]
        public async Task<ActionResult<NotificationDTO>> UpdateNotification([FromBody] NotificationDTO notification)
        {
            try
            {
                var returnedNotification = await _notificationService.UpdateNotification(notification);
                return Ok(returnedNotification);
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(string userId)
        {
            try
            {
                var notification = await _notificationService.GetNotification(userId);
                return Ok(notification);
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
