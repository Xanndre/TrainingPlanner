using Microsoft.AspNetCore.Builder;
using TrainingPlanner.API.Hubs;

namespace TrainingPlanner.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void ConfigureSignalR(this IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });
        }
    }
}
