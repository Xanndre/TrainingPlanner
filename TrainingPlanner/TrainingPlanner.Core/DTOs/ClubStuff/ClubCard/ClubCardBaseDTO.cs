using System;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubCard
{
    public class ClubCardBaseDTO
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public UserDTO User { get; set; }
        public bool UnlimitedValidityPeriod { get; set; }
        public bool UnlimitedEntries { get; set; }
    }
}
