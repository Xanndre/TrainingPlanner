﻿using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class PagedClubsDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<ClubBaseDTO> Clubs { get; set; }
    }
}
