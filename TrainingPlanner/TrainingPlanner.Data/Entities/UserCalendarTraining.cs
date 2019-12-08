using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class UserCalendarTraining
    {
        [Key]
        public int Id { get; set; }
        public int UserTrainingId { get; set; }
        public UserTraining UserTraining { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
    }
}
