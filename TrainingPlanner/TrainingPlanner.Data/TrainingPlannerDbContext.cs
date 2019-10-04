using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Data
{
    public class TrainingPlannerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerPrice> TrainerPrices { get; set; }
        public DbSet<TrainerRate> TrainerRatings { get; set; }
        public DbSet<TrainerSport> TrainerSports { get; set; }
        public TrainingPlannerDbContext(DbContextOptions<TrainingPlannerDbContext> options) : base(options)
        {

        }
    }
}
