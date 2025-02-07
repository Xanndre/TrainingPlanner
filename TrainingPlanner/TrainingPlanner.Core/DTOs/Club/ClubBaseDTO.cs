﻿using TrainingPlanner.Core.DTOs.Stuff;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Club
{
    public class ClubBaseDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PictureDTO Picture { get; set; }
        public bool IsFavourite { get; set; }
    }
}
