namespace TrainingPlanner.Core.DTOs
{
    public class TrainerPriceBasicDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
    }
}
