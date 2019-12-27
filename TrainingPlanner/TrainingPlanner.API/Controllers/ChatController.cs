using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanner.Core.DTOs.Chat;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatDTO>> CreateChat([FromBody] ChatCreateDTO chat)
        {
            try
            {
                var returnedChat = await _chatService.CreateChat(chat);
                return Ok(returnedChat);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetAllChats()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                return Ok(await _chatService.GetAllChats(userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("{chatId}/messages")]
        public async Task<ActionResult<PagedMessagesDTO>> GetAllMessages(
            [FromQuery] int chatId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                return Ok(await _chatService.GetAllMessages(pageNumber, pageSize, chatId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("partner/{receiverId}")]
        public async Task<ActionResult> GetChat(string receiverId)
        {
            try
            {
                var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var chat = await _chatService.GetChat(senderId, receiverId);

                return Ok(chat);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetChatById(int id)
        {
            try
            {
                var chat = await _chatService.GetChatById(id);
                return Ok(chat);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
