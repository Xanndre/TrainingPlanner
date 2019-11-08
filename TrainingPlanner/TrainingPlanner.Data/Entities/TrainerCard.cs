using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class TrainerCard
    {
        [Key]
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EntriesLeft { get; set; }
        public string TrainerName { get; set; }
    }
}
