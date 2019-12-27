using System;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Chat
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string ReceiverProfilePic { get; set; }
        public string SenderProfilePic { get; set; }
        public DateTime LastMessage { get; set; }
    }
}
