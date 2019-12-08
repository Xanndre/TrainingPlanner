using System;

namespace TrainingPlanner.Core.DTOs.UserStuff.UserCalendarTraining
{
    public class UserCalendarTrainingCreateDTO
    {
        public int UserTrainingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
    }
}
