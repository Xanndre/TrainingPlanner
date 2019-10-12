using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Description { get; set; }
        public ICollection<TrainerSportBasicDTO> Sports { get; set; }
    }
}
