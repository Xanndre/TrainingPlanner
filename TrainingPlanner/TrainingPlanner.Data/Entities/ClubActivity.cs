using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubActivity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Calories { get; set; }
        public string Level { get; set; }
        public string Picture { get; set; }
    }
}
