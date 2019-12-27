namespace TrainingPlanner.Core.DTOs.Chat
{
    public class MessageBaseDTO
    {
        public string Content { get; set; }
        public string SenderId { get; set; }
        public int ChatId { get; set; }
        public string ReceiverId { get; set; }
    }
}
