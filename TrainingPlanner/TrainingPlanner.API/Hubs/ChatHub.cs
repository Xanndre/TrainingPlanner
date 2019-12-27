using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Chat;
using TrainingPlanner.Core.Interfaces;

namespace TrainingPlanner.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(MessageBaseDTO message)
        {
            try
            {
                MessageDTO createdMessage = await _chatService.CreateMessage(message);
                await Clients.User(message.ReceiverId).SendAsync("MessageReceived", createdMessage, message.ChatId, message.SenderId);
                await Clients.Caller.SendAsync("MessageSent", createdMessage, message.ChatId);
            }
            catch (Exception exception)
            {
                await Clients.Caller.SendAsync("Error", exception.Message);
            }
        }
    }
}
