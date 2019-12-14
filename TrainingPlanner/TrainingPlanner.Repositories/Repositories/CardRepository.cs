using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class CardRepository : BaseRepository, ICardRepository
    {
        public CardRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<TrainerCard> GetTrainerCard(int id)
        {
            return await GetTrainerCardQuery()
                .FirstAsync(t => t.Id == id);
        }

        public async Task<TrainerCard> UpdateTrainerCard(TrainerCard card)
        {
            _trainingPlannerDbContext.Update(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return card;
        }

        public async Task<TrainerCard> CreateTrainerCard(TrainerCard card)
        {
            await _trainingPlannerDbContext.TrainerCards.AddAsync(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return card;
        }

        public async Task DeleteTrainerCard(TrainerCard card)
        {
            _trainingPlannerDbContext.TrainerCards.Remove(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainerCard>> GetUserTrainerCards(string userId)
        {
            return await GetTrainerCardQuery()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerCard>> GetTrainerTrainerCards(int trainerId)
        {
            return await GetTrainerCardQuery()
                .Where(c => c.TrainerId == trainerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerCard>> GetTrainerCards(string userId, int trainerId)
        {
            return await GetTrainerCardQuery()
                .Where(c => c.TrainerId == trainerId && c.UserId == userId)
                .ToListAsync();
        }

        public async Task<ClubCard> GetClubCard(int id)
        {
            return await GetClubCardQuery()
                .FirstAsync(t => t.Id == id);
        }

        public async Task<ClubCard> UpdateClubCard(ClubCard card)
        {
            _trainingPlannerDbContext.Update(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return card;
        }

        public async Task<ClubCard> CreateClubCard(ClubCard card)
        {
            await _trainingPlannerDbContext.ClubCards.AddAsync(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return card;
        }

        public async Task DeleteClubCard(ClubCard card)
        {
            _trainingPlannerDbContext.ClubCards.Remove(card);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClubCard>> GetUserClubCards(string userId)
        {
            var xd = await GetClubCardQuery()
                .Where(c => c.UserId == userId)
                .ToListAsync();
            return xd;
        }

        public async Task<IEnumerable<ClubCard>> GetClubClubCards(int clubId)
        {
            return await GetClubCardQuery()
                .Where(c => c.ClubId == clubId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClubCard>> GetClubCards(string userId, int clubId)
        {
            return await GetClubCardQuery()
                .Where(c => c.ClubId == clubId && c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetClubCardNames(int clubId)
        {
            var names = await _trainingPlannerDbContext.ClubCards
                .Where(u => u.Name != null && u.ClubId == clubId)
                .Select(u => u.Name)
                .Distinct()
                .ToListAsync();
            return names;
        }

        public async Task<IEnumerable<string>> GetTrainerCardNames(int trainerId)
        {
            var names = await _trainingPlannerDbContext.TrainerCards
                .Where(u => u.Name != null && u.TrainerId == trainerId)
                .Select(u => u.Name)
                .Distinct()
                .ToListAsync();
            return names;
        }

        private IQueryable<ClubCard> GetClubCardQuery()
        {
            return _trainingPlannerDbContext.ClubCards;
        }

        private IQueryable<TrainerCard> GetTrainerCardQuery()
        {
            return _trainingPlannerDbContext.TrainerCards;
        }
    }
}
