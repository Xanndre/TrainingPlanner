using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedPartnersDTO : PagedDTO
    {
        public IEnumerable<PartnerDTO> Partners { get; set; }
    }
}
