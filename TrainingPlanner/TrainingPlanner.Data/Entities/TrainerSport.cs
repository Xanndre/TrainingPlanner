using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class TrainerSport
    {
        [Key]
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }

    }
}
