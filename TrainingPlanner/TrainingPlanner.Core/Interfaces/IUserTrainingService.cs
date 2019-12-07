using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.UserStuff.UserTraining;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IUserTrainingService
    {
        Task<UserTrainingDTO> UpdateUserTraining(UserTrainingDTO training);
        Task<UserTrainingCreateDTO> CreateUserTraining(UserTrainingCreateDTO training);
        Task DeleteUserTraining(int id);
        Task<UserTrainingDTO> GetUserTraining(int id);
        Task<PagedUserTrainingsDTO> GetAllUserTrainings(int pageNumber, int pageSize, string userId);

    }
}
