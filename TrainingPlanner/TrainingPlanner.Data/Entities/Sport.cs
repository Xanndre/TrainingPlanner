using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Sport
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrainerSport> Trainers { get; set; }
    }
}
