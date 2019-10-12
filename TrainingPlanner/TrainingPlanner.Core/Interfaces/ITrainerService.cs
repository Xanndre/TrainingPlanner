using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDTO>> GetAllTrainers();
        Task<TrainerDTO> GetTrainer(int id);
        Task<TrainerDTO> GetTrainerByUser(string userId);
        Task<TrainerDTO> UpdateTrainer(TrainerDTO trainer);
        Task<TrainerCreateDTO> CreateTrainer(TrainerCreateDTO trainer);
        Task DeleteTrainer(int id);
    }
}
