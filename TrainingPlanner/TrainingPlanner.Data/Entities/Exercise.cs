using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Series { get; set; }
        public int? Repetitions { get; set; }
        public int? Duration { get; set; }
        public int UserTrainingId { get; set; }
        public UserTraining UserTraining { get; set; }
    }
}
