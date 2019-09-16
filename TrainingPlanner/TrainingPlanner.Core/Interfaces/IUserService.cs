using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUser(string id);
        Task<UserDTO> UpdateUser(UserDTO dto);
    }
}
