using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedClubCardsDTO : PagedDTO
    {
        public IEnumerable<ClubCardBaseDTO> Cards { get; set; }
    }
}
