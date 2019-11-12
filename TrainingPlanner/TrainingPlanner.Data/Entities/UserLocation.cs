using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class UserLocation
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Location { get; set; }
    }
}
