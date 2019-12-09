using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.UserStuff.UserCalendarTraining;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IUserCalendarTrainingService
    {
        Task<UserCalendarTrainingDTO> GetUserCalendarTraining(int id);
        Task<UserCalendarTrainingUpdateDTO> UpdateUserCalendarTraining(UserCalendarTrainingUpdateDTO training);
        Task<UserCalendarTrainingCreateDTO> CreateUserCalendarTraining(UserCalendarTrainingCreateDTO training);
        Task DeleteUserCalendarTraining(int id);
        Task<IEnumerable<UserCalendarTrainingDTO>> GetUserCalendarTrainings(string userId);
        Task<IEnumerable<UserCalendarTrainingCreateDTO>> CreateUserCalendarTrainingRange(IEnumerable<UserCalendarTrainingCreateDTO> trainings);
    }
}
