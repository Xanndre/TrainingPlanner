using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class TrainerCreateDTO
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public ICollection<TrainerSportDTO> Sports { get; set; }
    }
}
