using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate
{
    public class TrainerRateCreateDTO : RateDTO
    {
        public string UserId { get; set; }
        public int TrainerId { get; set; }
    }
}
