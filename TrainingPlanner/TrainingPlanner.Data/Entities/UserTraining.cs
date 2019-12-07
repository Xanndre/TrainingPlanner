using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class UserTraining
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
