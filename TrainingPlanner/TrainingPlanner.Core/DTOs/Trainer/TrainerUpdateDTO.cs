using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.TrainerStuff;

namespace TrainingPlanner.Core.DTOs.Trainer
{
    public class TrainerUpdateDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerSportBasicDTO> Sports { get; set; }
        public ICollection<TrainerPriceBasicDTO> PriceList { get; set; }
    }
}
