﻿using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.TrainerStuff;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Trainer
{
    public class TrainerBaseDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerSportDTO> Sports { get; set; }
        public bool IsFavourite { get; set; }
    }
}
