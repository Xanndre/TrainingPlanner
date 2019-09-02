using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Data
{
    public class TrainingPlannerDbContext: IdentityDbContext<ApplicationUser>
    {
        public TrainingPlannerDbContext(DbContextOptions<TrainingPlannerDbContext> options): base(options)
        {

        }
    }
}
