using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ICardService
    {
        Task<TrainerCardUpdateDTO> UpdateTrainerCard(TrainerCardUpdateDTO card, bool isDeactivating);
        Task<TrainerCardCreateDTO> CreateTrainerCard(TrainerCardCreateDTO card);
        Task DeleteTrainerCard(int id);
        Task<TrainerCardDTO> GetTrainerCard(int id);
        Task<PagedTrainerCardsDTO> GetUserTrainerCards(int pageNumber, int pageSize, string userId);
        Task<PagedTrainerCardsDTO> GetTrainerTrainerCards(int pageNumber, int pageSize, int trainerId);
        Task<PagedTrainerCardsDTO> GetTrainerCards(int pageNumber, int pageSize, string userId, int trainerId);
        Task<ClubCardUpdateDTO> UpdateClubCard(ClubCardUpdateDTO card, bool isDeactivating);
        Task<ClubCardCreateDTO> CreateClubCard(ClubCardCreateDTO card);
        Task DeleteClubCard(int id);
        Task<ClubCardDTO> GetClubCard(int id);
        Task<PagedClubCardsDTO> GetUserClubCards(int pageNumber, int pageSize, string userId);
        Task<PagedClubCardsDTO> GetClubClubCards(int pageNumber, int pageSize, int clubId);
        Task<PagedClubCardsDTO> GetClubCards(int pageNumber, int pageSize, string userId, int clubId);
    }
}
