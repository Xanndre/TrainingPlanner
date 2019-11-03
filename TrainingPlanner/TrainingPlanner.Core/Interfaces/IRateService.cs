using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubRate;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IRateService
    {
        Task<ClubRateDTO> GetClubRate(string userId, int clubId);
        Task<ClubRateDTO> UpdateClubRate(ClubRateDTO rate);
        Task<ClubRateCreateDTO> CreateClubRate(ClubRateCreateDTO rate);
        Task DeleteClubRate(int id);
        Task<PagedClubRatesDTO> GetAllClubRates(int pageNumber, int pageSize, int clubId);
        Task<TrainerRateDTO> GetTrainerRate(string userId, int trainerId);
        Task<TrainerRateDTO> UpdateTrainerRate(TrainerRateDTO rate);
        Task<TrainerRateCreateDTO> CreateTrainerRate(TrainerRateCreateDTO rate);
        Task DeleteTrainerRate(int id);
        Task<PagedTrainerRatesDTO> GetAllTrainerRates(int pageNumber, int pageSize, int trainerId);
    }
}
