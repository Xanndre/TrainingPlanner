using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await _trainingPlannerDbContext.Trainers.ToListAsync();
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

    }
}
