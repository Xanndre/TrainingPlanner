using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.UserStuff;

namespace TrainingPlanner.Core.DTOs.User
{
    public class PartnerDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }
        public double Similarity { get; set; }
        public ICollection<UserSportDTO> Sports { get; set; }
        public ICollection<UserLocationDTO> Locations { get; set; }
    }
}
