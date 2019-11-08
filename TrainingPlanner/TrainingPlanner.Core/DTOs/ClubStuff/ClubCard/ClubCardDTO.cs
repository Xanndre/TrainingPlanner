using System;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardDTO : ClubCardBaseDTO
    {
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string EntriesLeft { get; set; }
    }
}
