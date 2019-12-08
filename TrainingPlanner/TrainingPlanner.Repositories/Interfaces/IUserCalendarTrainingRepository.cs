using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IUserCalendarTrainingRepository
    {
        Task<UserCalendarTraining> UpdateUserCalendarTraining(UserCalendarTraining training);
        Task<UserCalendarTraining> CreateUserCalendarTraining(UserCalendarTraining training);
        Task DeleteUserCalendarTraining(UserCalendarTraining training);
        Task<IEnumerable<UserCalendarTraining>> GetUserCalendarTrainings(string userId);
        Task<UserCalendarTraining> GetUserCalendarTraining(int id);
        Task<IEnumerable<UserCalendarTraining>> CreateUserCalendarTrainingRange(IEnumerable<UserCalendarTraining> trainings);
    }
}
