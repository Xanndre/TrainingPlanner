using System;

namespace TrainingPlanner.Core.DTOs.Training
{
    public class TrainingCreateDTO
    {
        public string Title { get; set; }
        public int? ClubId { get; set; }
        public int? TrainerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainerName { get; set; }
        public string Room { get; set; }
        public string Level { get; set; }
        public int Entries { get; set; }
        public int EntriesLeft { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
    }
}
