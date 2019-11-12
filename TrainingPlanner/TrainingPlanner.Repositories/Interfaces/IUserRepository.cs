using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAllUsers();
        Task DeleteUser(ApplicationUser user);
        Task<ApplicationUser> GetUser(string id);
        Task<ApplicationUser> UpdateUser(ApplicationUser entity);
        Task<IdentityUserLogin<string>> GetUserLogin(string provider, string id);
        Task<IEnumerable<string>> GetLocations();
    }
}
