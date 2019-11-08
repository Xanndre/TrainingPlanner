namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardCreateDTO
    {
        public int ClubId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
        public string EntriesLeft { get; set; }
        public string ClubName { get; set; }
    }
}
