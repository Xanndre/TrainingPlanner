using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.Trainer;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ITrainerService
    {
        Task<TrainerDTO> GetTrainer(int id, bool isIncrementingViewCounter);
        Task<TrainerDTO> GetTrainerByUser(string userId);
        Task<TrainerUpdateDTO> UpdateTrainer(TrainerUpdateDTO trainer);
        Task<TrainerCreateDTO> CreateTrainer(TrainerCreateDTO trainer);
        Task DeleteTrainer(int id);
        Task<PagedTrainersDTO> GetAllTrainers(int pageNumber, int pageSize, string userId);
        Task<PagedTrainersDTO> GetFavouriteTrainers(int pageNumber, int pageSize, string userId);
    }
}
