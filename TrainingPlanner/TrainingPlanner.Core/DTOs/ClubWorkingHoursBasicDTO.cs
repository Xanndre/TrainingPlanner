using System;

namespace TrainingPlanner.Core.DTOs
{
    public class ClubWorkingHoursBasicDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }
        public int ClubId { get; set; }
    }
}
