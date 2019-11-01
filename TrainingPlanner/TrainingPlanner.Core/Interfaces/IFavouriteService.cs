using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Favourite;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IFavouriteService
    {
        Task<FavouriteClubDTO> CreateFavouriteClub(FavouriteClubDTO favourite);
        Task DeleteFavouriteClub(int clubId, string userId);
        Task<FavouriteTrainerDTO> CreateFavouriteTrainer(FavouriteTrainerDTO favourite);
        Task DeleteFavouriteTrainer(int trainerId, string userId);

    }
}
