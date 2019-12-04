using System;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardBaseDTO
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
