using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedUsersDTO : PagedDTO
    {
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
