using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.UserStuff.UserTraining;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedUserTrainingsDTO : PagedDTO
    {
        public IEnumerable<UserTrainingBaseDTO> UserTrainings { get; set; }
    }
}
