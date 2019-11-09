using System;
using TrainingPlanner.Core.DTOs.Trainer;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard
{
    public class TrainerCardBaseDTO
    {
        public int Id { get; set; }
        public string TrainerName { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserDTO User { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
