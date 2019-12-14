using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<TrainerCard> GetTrainerCard(int id);
        Task<TrainerCard> UpdateTrainerCard(TrainerCard card);
        Task<TrainerCard> CreateTrainerCard(TrainerCard card);
        Task DeleteTrainerCard(TrainerCard card);
        Task<IEnumerable<TrainerCard>> GetUserTrainerCards(string userId);
        Task<IEnumerable<TrainerCard>> GetTrainerTrainerCards(int trainerId);
        Task<IEnumerable<TrainerCard>> GetTrainerCards(string userId, int trainerId);
        Task<ClubCard> GetClubCard(int id);
        Task<ClubCard> UpdateClubCard(ClubCard card);
        Task<ClubCard> CreateClubCard(ClubCard card);
        Task DeleteClubCard(ClubCard card);
        Task<IEnumerable<ClubCard>> GetUserClubCards(string userId);
        Task<IEnumerable<ClubCard>> GetClubClubCards(int clubId);
        Task<IEnumerable<ClubCard>> GetClubCards(string userId, int clubId);
        Task<IEnumerable<string>> GetClubCardNames(int clubId);
        Task<IEnumerable<string>> GetTrainerCardNames(int trainerId);
    }
}
