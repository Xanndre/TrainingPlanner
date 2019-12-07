namespace TrainingPlanner.Core.DTOs.UserStuff.UserTraining
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Series { get; set; }
        public int? Repetitions { get; set; }
        public int? Duration { get; set; }
        public int UserTrainingId { get; set; }
    }
}
