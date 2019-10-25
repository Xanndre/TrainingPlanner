using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class PagedTrainersDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TrainerBaseDTO> Trainers { get; set; }
    }
}
