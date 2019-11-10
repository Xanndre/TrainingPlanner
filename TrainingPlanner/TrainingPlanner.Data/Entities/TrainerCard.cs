using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class TrainerCard
    {
        [Key]
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int EntriesLeft { get; set; }
        public string TrainerName { get; set; }
        public string UserName { get; set; }
    }
}
