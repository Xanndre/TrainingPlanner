using TrainingPlanner.Core.DTOs.Club;
using TrainingPlanner.Core.DTOs.Trainer;

namespace TrainingPlanner.Core.DTOs.Training
{
    public class TrainingDTO : TrainingUpdateDTO
    {
        public TrainerBaseDTO Trainer { get; set; }
        public ClubBaseDTO Club { get; set; }
        public int EntriesLeft { get; set; }
    }
}
