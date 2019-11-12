using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class UserSport
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Sport { get; set; }
    }
}
