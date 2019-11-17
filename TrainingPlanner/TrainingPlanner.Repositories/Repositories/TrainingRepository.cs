using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class TrainingRepository : BaseRepository, ITrainingRepository
    {
        public TrainingRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Training> GetTraining(int id)
        {
            return await _trainingPlannerDbContext.Trainings
                .FirstAsync(t => t.Id == id);
        }

        public async Task<Training> UpdateTraining(Training training)
        {
            _trainingPlannerDbContext.Update(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task<Training> CreateTraining(Training training)
        {
            await _trainingPlannerDbContext.Trainings.AddAsync(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return training;
        }

        public async Task DeleteTraining(Training training)
        {
            _trainingPlannerDbContext.Trainings.Remove(training);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Training>> GetTrainerTrainings(int trainerId)
        {
            var trainings = GetTrainingQuery()
                .Where(c => c.TrainerId == trainerId);
            return await trainings.ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetClubTrainings(int clubId)
        {
            var trainings = GetTrainingQuery()
                .Where(c => c.ClubId == clubId);
            return await trainings.ToListAsync();
        }

        private IQueryable<Training> GetTrainingQuery()
        {
            return _trainingPlannerDbContext.Trainings;
        }
    }
}
