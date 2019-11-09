using System;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardUpdateDTO : ClubCardCreateDTO
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
