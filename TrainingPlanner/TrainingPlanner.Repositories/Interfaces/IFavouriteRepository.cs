using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<FavouriteClub> CreateFavouriteClub(FavouriteClub favourite);
        Task DeleteFavouriteClub(FavouriteClub favourite);
        Task<FavouriteClub> GetFavouriteClub(int clubId, string userId);
        Task<FavouriteTrainer> CreateFavouriteTrainer(FavouriteTrainer favourite);
        Task DeleteFavouriteTrainer(FavouriteTrainer favourite);
        Task<FavouriteTrainer> GetFavouriteTrainer(int trainerId, string userId);
        Task<IEnumerable<FavouriteClub>> GetUserFavouriteClubs(string userId);
        Task<IEnumerable<FavouriteTrainer>> GetUserFavouriteTrainers(string userId);
        Task<IEnumerable<FavouriteClub>> GetFavouriteClubs(int clubId);
        Task<IEnumerable<FavouriteTrainer>> GetFavouriteTrainers(int trainerId);
    }
}
