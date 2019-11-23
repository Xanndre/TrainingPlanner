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
                .Include(t => t.Club)
                .ThenInclude(t => t.User)
                .Include(t => t.Trainer)
                .ThenInclude(t => t.User)
                .Include(t => t.Reservations)
                .AsNoTracking()
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

        public async Task<IEnumerable<Training>> GetReservedTrainings(string userId)
        {
            var trainings = GetTrainingWithReservationQuery(userId)
                .Where(t => _trainingPlannerDbContext.Reservations
                    .Any(res => t.Id == res.TrainingId && res.User.Id == userId));

            return await trainings.ToList();
        }

        private IQueryable<Training> GetTrainingQuery()
        {
            return _trainingPlannerDbContext.Trainings;
        }

        private IAsyncEnumerable<Training> GetTrainingWithReservationQuery(string userId)
        {
            return GetTrainingQuery()
                  .Include(x => x.Reservations)
                  .ToAsyncEnumerable()
                  .Select(x =>
                  {
                      x.Reservations = x.Reservations.Any() ? x.Reservations.Where(f => f.UserId == userId).ToList() : null;
                      return x;
                  });
        }
    }
}
