using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task<Training> GetTraining(int id);
        Task<Training> UpdateTraining(Training training);
        Task<Training> CreateTraining(Training training);
        Task DeleteTraining(Training training);
        Task<IEnumerable<Training>> GetTrainerTrainings(int trainerId);
        Task<IEnumerable<Training>> GetClubTrainings(int clubId);
    }
}
