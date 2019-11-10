namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardCreateDTO
    {
        public int ClubId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public int EntriesLeft { get; set; }
        public string ClubName { get; set; }
        public string UserName { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
