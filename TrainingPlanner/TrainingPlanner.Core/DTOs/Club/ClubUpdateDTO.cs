﻿namespace TrainingPlanner.Core.DTOs.Club
{
    public class ClubUpdateDTO : ClubCreateDTO
    {
        public int Id { get; set; }
        public int ViewCounter { get; set; }
    }
}
