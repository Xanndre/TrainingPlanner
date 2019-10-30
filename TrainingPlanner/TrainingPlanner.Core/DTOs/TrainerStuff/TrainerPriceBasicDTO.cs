using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.TrainerStuff
{
    public class TrainerPriceBasicDTO : PriceBasicDTO
    {
        public int TrainerId { get; set; }
    }
}
