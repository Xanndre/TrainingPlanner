using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedReservationUsersDTO : PagedDTO
    {
        public TrainingDTO Training { get; set; }
        public IEnumerable<ReservationUserDTO> Users { get; set; }
    }
}
