using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Club;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedClubsDTO : PagedDTO
    {
        public IEnumerable<ClubBaseDTO> Clubs { get; set; }
    }
}
