using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubRate;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedClubRatesDTO : PagedDTO
    {
        public IEnumerable<ClubRateBaseDTO> Rates { get; set; }
    }
}
