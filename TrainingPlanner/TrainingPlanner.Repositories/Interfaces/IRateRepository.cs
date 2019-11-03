using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IRateRepository
    {
        Task<TrainerRate> UpdateTrainerRate(TrainerRate rate);
        Task<TrainerRate> CreateTrainerRate(TrainerRate rate);
        Task DeleteTrainerRate(TrainerRate rate);
        Task<TrainerRate> GetTrainerRate(string userId, int trainerId);
        Task<IEnumerable<TrainerRate>> GetTrainerRates(int trainerId);
        Task<TrainerRate> GetTrainerRateById(int id);
        Task<ClubRate> UpdateClubRate(ClubRate rate);
        Task<ClubRate> CreateClubRate(ClubRate rate);
        Task DeleteClubRate(ClubRate rate);
        Task<ClubRate> GetClubRate(string userId, int clubId);
        Task<IEnumerable<ClubRate>> GetClubRates(int clubId);
        Task<ClubRate> GetClubRateById(int id);
    }
}
