using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IUserTrainingRepository
    {
        Task<UserTraining> UpdateUserTraining(UserTraining training);
        Task<UserTraining> CreateUserTraining(UserTraining training);
        Task DeleteUserTraining(UserTraining training);
        Task<IEnumerable<UserTraining>> GetUserTrainings(string userId);
        Task<UserTraining> GetUserTraining(int id);
        Task<IEnumerable<Exercise>> GetExercisesToDelete(UserTraining training);
        Task RemoveExercises(IEnumerable<Exercise> exercises, bool isSavingChanges = true);
    }
}
