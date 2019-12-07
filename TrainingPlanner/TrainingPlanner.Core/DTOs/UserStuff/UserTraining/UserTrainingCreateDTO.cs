using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs.UserStuff.UserTraining
{
    public class UserTrainingCreateDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public ICollection<ExerciseDTO> Exercises { get; set; }
    }
}
