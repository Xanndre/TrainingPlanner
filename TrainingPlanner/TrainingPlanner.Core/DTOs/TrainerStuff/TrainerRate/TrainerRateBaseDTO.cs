using TrainingPlanner.Core.DTOs.Stuff;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate
{
    public class TrainerRateBaseDTO : RateDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public int TrainerId { get; set; }
    }
}
