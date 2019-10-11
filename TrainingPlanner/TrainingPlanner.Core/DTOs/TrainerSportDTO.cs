namespace TrainingPlanner.Core.DTOs
{
    public class TrainerSportDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public TrainerDTO Trainer { get; set; }
        public int SportId { get; set; }
        public SportDTO Sport { get; set; }

    }
}
