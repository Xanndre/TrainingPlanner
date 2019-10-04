using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class TrainerRate
    {
        [Key]
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
    }
}
