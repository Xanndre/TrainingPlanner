using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            await _trainingPlannerDbContext.Reservations.AddAsync(reservation);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            _trainingPlannerDbContext.Update(reservation);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> UpdateRange(IEnumerable<Reservation> reservations)
        {
            _trainingPlannerDbContext.UpdateRange(reservations);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return reservations;
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            _trainingPlannerDbContext.Reservations.Remove(reservation);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }
        public async Task<Reservation> GetReservation(int trainingId, string userId)
        {
            return await GetReservations()
                .FirstOrDefaultAsync(res => res.TrainingId == trainingId && res.UserId == userId);
        }

        public async Task<IEnumerable<Reservation>> GetReservations(string userId)
        {
            return await GetReservations()
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsOnTraining(int trainingId)
        {
            return await GetReservations()
                .Include(t => t.User)
                    .ThenInclude(u => u.TrainerCards)
                .Include(t => t.User)
                    .ThenInclude(u => u.ClubCards)
                .Include(t => t.User)
                    .ThenInclude(u => u.Notification)
                .Where(t => t.TrainingId == trainingId)
                .ToListAsync();
        }

        private IQueryable<Reservation> GetReservations()
        {
            return _trainingPlannerDbContext.Reservations;
        }
    }
}
