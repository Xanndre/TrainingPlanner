using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class TrainerBaseDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerSportDTO> Sports { get; set; }
        public bool IsFavourite { get; set; }
    }
}
