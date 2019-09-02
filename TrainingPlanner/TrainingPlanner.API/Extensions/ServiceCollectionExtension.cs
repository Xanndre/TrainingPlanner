using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<TrainingPlannerDbContext>()
                .AddDefaultUI();
        }
    }
}
