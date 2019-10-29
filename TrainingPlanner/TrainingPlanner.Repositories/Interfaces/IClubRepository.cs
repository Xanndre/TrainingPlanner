using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IClubRepository
    {
        Task<Club> GetClub(int id);
        Task<int> GetClubQuantity(string userId);
        Task<IEnumerable<int>> GetClubIds(string userId);
        Task<Club> UpdateClub(Club club);
        Task<Club> CreateClub(Club club);
        Task DeleteClub(Club club);
        IQueryable<Club> GetAllClubs();
        Task<IEnumerable<Club>> GetFavouriteClubs(string userId);
        Task<IEnumerable<Club>> GetAllClubs(string userId);
        Task<IEnumerable<Club>> GetUserClubs(string userId);
        Task<IEnumerable<ClubActivity>> GetClubActivitiesToDelete(Club club);
        Task<IEnumerable<ClubTrainer>> GetClubTrainersToDelete(Club club);
        Task<IEnumerable<Picture>> GetClubPicturesToDelete(Club club);
        Task<IEnumerable<ClubPrice>> GetClubPricesToDelete(Club club);
        Task<IEnumerable<ClubWorkingHours>> GetClubWorkingHoursToDelete(Club club);
        Task RemoveClubActivities(IEnumerable<ClubActivity> activities, bool isSavingChanges = true);
        Task RemoveClubTrainers(IEnumerable<ClubTrainer> trainers, bool isSavingChanges = true);
        Task RemoveClubPictures(IEnumerable<Picture> pictures, bool isSavingChanges = true);
        Task RemoveClubPrices(IEnumerable<ClubPrice> priceList, bool isSavingChanges = true);
        Task RemoveClubWorkingHours(IEnumerable<ClubWorkingHours> workingHours, bool isSavingChanges = true);
    }
}
