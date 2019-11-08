using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedTrainerCardsDTO : PagedDTO
    {
        public IEnumerable<TrainerCardBaseDTO> Cards { get; set; }
    }
}
