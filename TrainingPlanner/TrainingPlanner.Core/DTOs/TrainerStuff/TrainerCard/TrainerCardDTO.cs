namespace TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard
{
    public class TrainerCardDTO : TrainerCardBaseDTO
    {
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public int EntriesLeft { get; set; }
    }
}
