using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteReservation(Reservation reservation)
        {
            _trainingPlannerDbContext.Reservations.Remove(reservation);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }
        public async Task<Reservation> GetReservation(int trainingId, string userId)
        {
            return await GetReservations()
                .FirstAsync(res => res.TrainingId == trainingId && res.UserId == userId);
        }

        private IQueryable<Reservation> GetReservations()
        {
            return _trainingPlannerDbContext.Reservations;
        }
    }
}
