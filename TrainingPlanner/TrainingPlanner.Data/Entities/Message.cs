using System;

namespace TrainingPlanner.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        
    }
}
