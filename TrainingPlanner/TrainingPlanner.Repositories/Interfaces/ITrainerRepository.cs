using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllTrainers();
        Task<Trainer> GetTrainer(int id);
        Task<Trainer> GetTrainerByUser(string userId);
        Task<Trainer> UpdateTrainer(Trainer trainer);
        Task<Trainer> CreateTrainer(Trainer trainer);
        Task DeleteTrainer(Trainer trainer);
    }
}
