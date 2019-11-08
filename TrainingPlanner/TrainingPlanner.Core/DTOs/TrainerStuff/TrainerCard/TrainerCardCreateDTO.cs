namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard
{
    public class TrainerCardCreateDTO
    {
        public int TrainerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
        public string EntriesLeft { get; set; }
        public string TrainerName { get; set; }
    }
}
