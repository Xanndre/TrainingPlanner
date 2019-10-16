using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class TrainerRepository : BaseRepository, ITrainerRepository
    {
        public TrainerRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Trainer>> GetAllTrainers()
        {
            return await _trainingPlannerDbContext.Trainers
                .Include(t => t.User)
                .Include(t => t.Sports)
                .ThenInclude(s => s.Sport)
                .ToListAsync();
        }

        public async Task<Trainer> GetTrainer(int id)
        {
            return await _trainingPlannerDbContext.Trainers.FirstAsync(t => t.Id == id);
        }

        public async Task<Trainer> GetTrainerByUser(string userId)
        {
            return await _trainingPlannerDbContext.Trainers
                .Include(t => t.PriceList)
                .Include(t => t.Sports)
                .ThenInclude(t => t.Sport)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<Trainer> UpdateTrainer(Trainer trainer)
        {
            _trainingPlannerDbContext.Update(trainer);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer> CreateTrainer(Trainer trainer)
        {
            await _trainingPlannerDbContext.Trainers.AddAsync(trainer);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return trainer;
        }

        public async Task DeleteTrainer(Trainer trainer)
        {
            _trainingPlannerDbContext.Trainers.Remove(trainer);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainerSport>> GetTrainerSportsToDelete(Trainer trainer)
        {
            return await _trainingPlannerDbContext.TrainerSports
                .Where(t => t.TrainerId == trainer.Id)
                .Except(trainer.Sports)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerPrice>> GetTrainerPricesToDelete(Trainer trainer)
        {
            return await _trainingPlannerDbContext.TrainerPrices
                .Where(t => t.TrainerId == trainer.Id)
                .Except(trainer.PriceList)
                .ToListAsync();
        }

        public async Task RemoveTrainerSports(IEnumerable<TrainerSport> sports, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.TrainerSports.RemoveRange(sports);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveTrainerPrices(IEnumerable<TrainerPrice> priceList, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.TrainerPrices.RemoveRange(priceList);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

    }
}
