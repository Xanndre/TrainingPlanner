using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubWorkingHours
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
