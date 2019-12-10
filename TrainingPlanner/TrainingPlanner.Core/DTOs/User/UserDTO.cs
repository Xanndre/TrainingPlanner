using System;
using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.BodyMeasurement;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.Notification;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;
using TrainingPlanner.Core.DTOs.UserStuff;

namespace TrainingPlanner.Core.DTOs.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<UserSportDTO> Sports { get; set; }
        public ICollection<UserLocationDTO> Locations { get; set; }
        public ICollection<BodyMeasurementDTO> BodyMeasurements { get; set; }
        public ICollection<ClubCardDTO> ClubCards { get; set; }
        public ICollection<TrainerCardDTO> TrainerCards { get; set; }
        public NotificationDTO Notification { get; set; }
    }
}
