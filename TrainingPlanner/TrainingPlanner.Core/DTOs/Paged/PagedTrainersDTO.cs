using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Trainer;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedTrainersDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TrainerBaseDTO> Trainers { get; set; }
    }
}
