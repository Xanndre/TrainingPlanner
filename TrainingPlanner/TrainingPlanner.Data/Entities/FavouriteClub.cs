using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class FavouriteClub
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? ClubId { get; set; }
        public Club Club { get; set; }
    }
}
