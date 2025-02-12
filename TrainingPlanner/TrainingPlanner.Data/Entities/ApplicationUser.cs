﻿using Microsoft.AspNetCore.Identity;
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
        public ICollection<BodyMeasurement> BodyMeasurements { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ClubCard> ClubCards { get; set; }
        public ICollection<TrainerCard> TrainerCards { get; set; }
        public ICollection<UserTraining> UserTrainings { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
