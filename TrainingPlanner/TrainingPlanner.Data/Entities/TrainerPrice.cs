using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class TrainerPrice
    {
        [Key]
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
    }
}
