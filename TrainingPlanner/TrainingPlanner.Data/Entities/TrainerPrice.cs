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
        public int ValidityPeriod { get; set; }
        public int Entries { get; set; }
        public double Price { get; set; }
    }
}
