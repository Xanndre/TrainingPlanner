using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IUserService
    {
        PagedUsersDTO GetAllUsers(int pageNumber, int pageSize);
        Task<UserDTO> GetUser(string id);
        Task<PartnerDTO> GetPartner(string id);
        Task<UserDTO> UpdateUser(UserDTO user, bool isPartner);
        Task DeleteUser(string id);
        Task<IEnumerable<string>> GetLocations();
        Task<PagedPartnersDTO> GetAllPartners(int pageNumber, int pageSize, string userId);
    }
}
