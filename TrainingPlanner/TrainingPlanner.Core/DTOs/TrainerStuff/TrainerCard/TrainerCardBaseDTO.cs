using System;

namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard
{
    public class TrainerCardBaseDTO
    {
        public int Id { get; set; }
        public string TrainerName { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
