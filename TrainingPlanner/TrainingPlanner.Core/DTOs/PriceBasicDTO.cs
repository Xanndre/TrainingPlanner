namespace TrainingPlanner.Core.DTOs
{
    public class PriceBasicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
    }
}
