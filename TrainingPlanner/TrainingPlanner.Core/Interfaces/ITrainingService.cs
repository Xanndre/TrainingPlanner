using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Training;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ITrainingService
    {
        Task<TrainingDTO> GetTraining(int id);
        Task<TrainingDTO> UpdateTraining(TrainingDTO training);
        Task<TrainingCreateDTO> CreateTraining(TrainingCreateDTO training);
        Task DeleteTraining(int id);
        Task<IEnumerable<TrainingDTO>> GetTrainerTrainings(int trainerId);
        Task<IEnumerable<TrainingDTO>> GetClubTrainings(int clubId);
    }
}
