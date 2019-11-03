using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubRate
    {
        [Key]
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
    }
}
