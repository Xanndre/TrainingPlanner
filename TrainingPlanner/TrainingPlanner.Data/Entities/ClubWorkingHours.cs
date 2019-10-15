using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubWorkingHours
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }
    }
}
