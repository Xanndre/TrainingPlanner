using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TrainingPlanner.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<UserSport> Sports { get; set; }
        public ICollection<UserLocation> Locations { get; set; }
    }
}
