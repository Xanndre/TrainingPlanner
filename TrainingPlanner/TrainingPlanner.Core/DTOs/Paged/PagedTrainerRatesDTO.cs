using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedTrainerRatesDTO : PagedDTO
    {
        public IEnumerable<TrainerRateBaseDTO> Rates { get; set; }
    }
}
