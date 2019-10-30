using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.TrainerStuff
{
    public class TrainerPriceDTO : PriceDTO
    {
        public int TrainerId { get; set; }
    }
}
