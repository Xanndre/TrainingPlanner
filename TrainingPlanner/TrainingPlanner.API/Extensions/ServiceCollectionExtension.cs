using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Mappings;
using TrainingPlanner.Core.Options;
using TrainingPlanner.Core.Services;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;
using TrainingPlanner.Repositories.Repositories;

namespace TrainingPlanner.API.Extensions
{
    public static class ServiceCollectionExtension
    {   
        public static void AddDefaultIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<TrainingPlannerDbContext>()
                .AddDefaultUI();
        }

        public static void ConfigureIdentityTokens(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromMinutes(10));
        }

        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:JwtSecret").Value)),
                    };
                });
        }

        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISportService, SportService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IBodyMeasurementService, BodyMeasurementService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<IReservationService, ReservationService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISportRepository, SportRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IBodyMeasurementRepository, BodyMeasurementRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.Configure<FacebookLoginOptions>(configuration.GetSection("FacebookLogin"));
            services.Configure<EmailOptions>(configuration.GetSection("Email"));
        }

        public static void AddDefaultCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
