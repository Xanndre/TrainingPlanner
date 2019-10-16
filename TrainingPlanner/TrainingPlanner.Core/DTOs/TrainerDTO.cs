using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerSportDTO> Sports { get; set; }
        public ICollection<TrainerPriceBasicDTO> PriceList { get; set; }
    }
}
