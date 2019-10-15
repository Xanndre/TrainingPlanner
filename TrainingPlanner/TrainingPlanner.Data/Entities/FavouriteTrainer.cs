using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class FavouriteTrainer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? TrainerId { get; set; }
        public Trainer Trainer { get; set; }
    }
}
