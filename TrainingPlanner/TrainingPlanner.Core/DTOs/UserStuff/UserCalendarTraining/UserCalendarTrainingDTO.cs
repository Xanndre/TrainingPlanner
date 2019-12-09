using TrainingPlanner.Core.DTOs.UserStuff.UserTraining;

namespace TrainingPlanner.Core.DTOs.UserStuff.UserCalendarTraining
{
    public class UserCalendarTrainingDTO : UserCalendarTrainingUpdateDTO
    {
        public UserTrainingDTO UserTraining { get; set; }
    }
}
