using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainingPlanner.API.Extensions;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data;

namespace TrainingPlanner.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<TrainingPlannerDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString(DictionaryResources.DbConnection)));

            services.AddRepositories();
            services.AddServices();
            services.AddMapper();
            services.AddDefaultCors();
            services.AddDefaultIdentity();
            services.ConfigureIdentityTokens();
            services.AddJwtAuth(Configuration);
            services.ConfigureOptions(Configuration);
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(DictionaryResources.AllowAllHeaders);
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
