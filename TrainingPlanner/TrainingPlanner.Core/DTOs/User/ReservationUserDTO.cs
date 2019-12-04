using TrainingPlanner.Core.DTOs.Reservation;

namespace TrainingPlanner.Core.DTOs.User
{
    public class ReservationUserDTO
    {
        public UserDTO User { get; set; }
        public ReservationInfoDTO ReservationInfo { get; set; }
    }
}
