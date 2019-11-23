using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ITrainingService
    {
        Task<TrainingDTO> GetTraining(int id);
        Task<TrainingUpdateDTO> UpdateTraining(TrainingUpdateDTO training);
        Task<TrainingCreateDTO> CreateTraining(TrainingCreateDTO training);
        Task DeleteTraining(int id);
        Task<IEnumerable<TrainingDTO>> GetTrainerTrainings(int trainerId);
        Task<IEnumerable<TrainingDTO>> GetClubTrainings(int clubId);
        Task<IEnumerable<TrainingDTO>> GetReservedTrainings(string userId);
        Task UpdateSignedUpList(Training training);
    }
}
