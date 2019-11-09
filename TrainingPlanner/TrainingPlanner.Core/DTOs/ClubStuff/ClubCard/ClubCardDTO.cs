using System;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardDTO : ClubCardBaseDTO
    {
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int EntriesLeft { get; set; }
    }
}
