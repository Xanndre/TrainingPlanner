using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;
using TrainingPlanner.Core.Helpers;

namespace TrainingPlanner.Core.Interfaces
{
    public interface ICardService
    {
        Task<TrainerCardUpdateDTO> UpdateTrainerCard(TrainerCardUpdateDTO card, bool isDeactivating);
        Task<TrainerCardCreateDTO> CreateTrainerCard(TrainerCardCreateDTO card);
        Task DeleteTrainerCard(int id);
        Task<TrainerCardDTO> GetTrainerCard(int id);
        Task<PagedTrainerCardsDTO> GetUserTrainerCards(int pageNumber, int pageSize, string userId,
            CardFilterData filterData);
        Task<PagedTrainerCardsDTO> GetTrainerTrainerCards(int pageNumber, int pageSize, int trainerId,
            CardFilterData filterData);
        Task<PagedTrainerCardsDTO> GetTrainerCards(int pageNumber, int pageSize, string userId, int trainerId,
            CardFilterData filterData);
        Task<ClubCardUpdateDTO> UpdateClubCard(ClubCardUpdateDTO card, bool isDeactivating);
        Task<ClubCardCreateDTO> CreateClubCard(ClubCardCreateDTO card);
        Task DeleteClubCard(int id);
        Task<ClubCardDTO> GetClubCard(int id);
        Task<PagedClubCardsDTO> GetUserClubCards(int pageNumber, int pageSize, string userId,
            CardFilterData filterData);
        Task<PagedClubCardsDTO> GetClubClubCards(int pageNumber, int pageSize, int clubId,
            CardFilterData filterData);
        Task<PagedClubCardsDTO> GetClubCards(int pageNumber, int pageSize, string userId, int clubId,
            CardFilterData filterData);
        Task DeleteCardEntries(int trainingId, int? trainerId, int? clubId);
        Task SendNotificationCardAlmostExpired(string email, string trainerName, string userFirstName, bool isChecked);
        Task SendNotificationCardExpired(string email, string trainerName, string userFirstName, bool isChecked);
        Task<IEnumerable<string>> GetClubCardNames(int clubId);
        Task<IEnumerable<string>> GetTrainerCardNames(int trainerId);
    }
}
