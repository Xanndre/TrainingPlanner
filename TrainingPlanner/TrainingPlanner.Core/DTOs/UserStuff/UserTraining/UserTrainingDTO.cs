using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs.UserStuff.UserTraining
{
    public class UserTrainingDTO : UserTrainingBaseDTO
    {
        public string UserId { get; set; }
        public ICollection<ExerciseDTO> Exercises { get; set; }
    }
}
