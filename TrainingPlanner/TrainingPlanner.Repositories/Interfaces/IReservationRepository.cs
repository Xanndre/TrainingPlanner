using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task DeleteReservation(Reservation reservation);
        Task<Reservation> GetReservation(int trainingId, string userId);
    }
}
