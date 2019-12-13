using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ITrainingService
    {
        Task<TrainingDTO> GetTraining(int id);
        Task<TrainingUpdateDTO> UpdateTraining(TrainingUpdateDTO training);
        Task<TrainingCreateDTO> CreateTraining(TrainingCreateDTO training);
        Task DeleteTraining(int id);
        Task<IEnumerable<TrainingDTO>> GetTrainerTrainings(int trainerId, TrainingFilterData filterData);
        Task<IEnumerable<TrainingDTO>> GetClubTrainings(int clubId, TrainingFilterData filterData);
        Task<IEnumerable<TrainingDTO>> GetReservedTrainings(string userId, TrainingFilterData filterData);
        Task UpdateSignedUpList(Training training);
        Task<IEnumerable<TrainingCreateDTO>> CreateTrainingRange(IEnumerable<TrainingCreateDTO> trainings);
        Task SendNotificationIncomingTraining(Training training);
        Task SendNotificationTrainingDeleted(Reservation reservation, Training training);
        Task SendNotificationListToReserveList(Reservation reservation, Training training);
    }
}
