using System;

namespace TrainingPlanner.Core.DTOs.Chat
{
    public class MessageDTO
    {
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderId { get; set; }
    }
}
