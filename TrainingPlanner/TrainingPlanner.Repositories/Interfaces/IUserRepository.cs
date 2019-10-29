using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task DeleteUser(ApplicationUser user);
        Task<ApplicationUser> GetUser(string id);
        Task<ApplicationUser> UpdateUser(ApplicationUser entity);
        Task<IdentityUserLogin<string>> GetUserLogin(string provider, string id);
    }
}
