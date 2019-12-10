using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Reservation;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDTO> CreateReservation(ReservationDTO reservation);
        Task DeleteReservation(int trainingId, string userId);
        Task<ReservationInfoDTO> GetReservationInfo(string userId, int trainingId);
        Task SendNotificationSignUpConfirmed(int reservationId, bool isReserveList);
        Task SendNotificationSignOutConfirmed(Reservation reservation, bool isReserveList);
    }
}
