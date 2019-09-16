﻿using Microsoft.AspNetCore.Identity;
using System;

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
    }
}
