using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Club;
using TrainingPlanner.Core.DTOs.Paged;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IClubService
    {
        Task<ClubDTO> GetClub(int id, bool isIncrementingViewCounter);
        Task<int> GetClubQuantity(string userId);
        Task<IEnumerable<int>> GetClubIds(string userId);
        Task<ClubUpdateDTO> UpdateClub(ClubUpdateDTO club);
        Task<ClubCreateDTO> CreateClub(ClubCreateDTO club);
        Task DeleteClub(int id);
        Task<PagedClubsDTO> GetAllClubs(int pageNumber, int pageSize, string userId);
        Task<PagedClubsDTO> GetUserClubs(int pageNumber, int pageSize, string userId);
        Task<PagedClubsDTO> GetFavouriteClubs(int pageNumber, int pageSize, string userId);
    }
}
