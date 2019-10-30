using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Trainer;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedTrainersDTO : PagedDTO
    {
        public IEnumerable<TrainerBaseDTO> Trainers { get; set; }
    }
}
