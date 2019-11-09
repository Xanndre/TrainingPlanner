namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard
{
    public class TrainerCardCreateDTO
    {
        public int TrainerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public int EntriesLeft { get; set; }
        public string TrainerName { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
