using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TrainerRate> Rating { get; set; }
        public ICollection<TrainerPrice> PriceList { get; set; }
        public ICollection<TrainerSport> Sports { get; set; }
        public ICollection<FavouriteTrainer> Favourites { get; set; }
        public int ViewCounter { get; set; }
    }
}
