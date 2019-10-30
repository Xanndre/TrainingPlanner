using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Club;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedClubsDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<ClubBaseDTO> Clubs { get; set; }
    }
}
