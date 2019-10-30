using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.TrainerStuff;

namespace TrainingPlanner.Core.DTOs.Trainer
{
    public class TrainerCreateDTO
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerSportDTO> Sports { get; set; }
        public ICollection<TrainerPriceDTO> PriceList { get; set; }
    }
}
