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
        Task<IEnumerable<TrainerSport>> GetTrainerSportsToDelete(Trainer trainer);
        Task<IEnumerable<TrainerPrice>> GetTrainerPricesToDelete(Trainer trainer);
        Task RemoveTrainerSports(IEnumerable<TrainerSport> sports, bool isSavingChanges = true);
        Task RemoveTrainerPrices(IEnumerable<TrainerPrice> priceList, bool isSavingChanges = true);
    }
}
