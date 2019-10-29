using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IClubService
    {
        Task<ClubDTO> GetClub(int id);
        Task<int> GetClubQuantity(string userId);
        Task<ClubUpdateDTO> UpdateClub(ClubUpdateDTO club);
        Task<ClubCreateDTO> CreateClub(ClubCreateDTO club);
        Task DeleteClub(int id);
        Task<PagedClubsDTO> GetAllClubs(int pageNumber, int pageSize, string userId);
        Task<PagedClubsDTO> GetUserClubs(int pageNumber, int pageSize, string userId);
        Task<PagedClubsDTO> GetFavouriteClubs(int pageNumber, int pageSize, string userId);
    }
}
