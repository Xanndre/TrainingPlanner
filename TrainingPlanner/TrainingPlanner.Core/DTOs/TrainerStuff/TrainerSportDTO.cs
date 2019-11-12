using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.TrainerStuff
{
    public class TrainerSportDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int SportId { get; set; }
        public SportDTO Sport { get; set; }
    }
}
