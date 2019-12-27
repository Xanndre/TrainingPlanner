using System.Collections.Generic;

namespace TrainingPlanner.Data.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
