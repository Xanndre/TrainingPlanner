namespace TrainingPlanner.Core.DTOs.Stuff
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
