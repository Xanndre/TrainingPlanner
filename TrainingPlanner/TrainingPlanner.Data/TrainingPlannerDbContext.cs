using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Data
{
    public class TrainingPlannerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<TrainerPrice> TrainerPrices { get; set; }
        public DbSet<ClubPrice> ClubPrices { get; set; }
        public DbSet<TrainerRate> TrainerRatings { get; set; }
        public DbSet<ClubRate> ClubRatings { get; set; }
        public DbSet<TrainerSport> TrainerSports { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<FavouriteClub> FavouriteClubs { get; set; }
        public DbSet<FavouriteTrainer> FavouriteTrainers { get; set; }
        public DbSet<ClubWorkingHours> ClubWorkingHours { get; set; }
        public DbSet<ClubActivity> ClubActivities { get; set; }
        public DbSet<ClubTrainer> ClubTrainers { get; set; }
        public DbSet<ClubCard> ClubCards { get; set; }
        public DbSet<TrainerCard> TrainerCards { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<UserSport> UserSports { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public DbSet<BodyInjury> BodyInjuries { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UserTraining> UserTrainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public TrainingPlannerDbContext(DbContextOptions<TrainingPlannerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrainerSport>()
                        .HasOne(ts => ts.Sport)
                        .WithMany(s => s.Trainers)
                        .HasForeignKey(ts => ts.SportId);

            modelBuilder.Entity<TrainerSport>()
                        .HasOne(ts => ts.Trainer)
                        .WithMany(t => t.Sports)
                        .HasForeignKey(ts => ts.TrainerId);

            modelBuilder.Entity<TrainerCard>()
                        .HasOne(t => t.Trainer)
                        .WithMany()
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ClubCard>()
                        .HasOne(t => t.Club)
                        .WithMany()
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TrainerCard>()
                        .HasOne(t => t.User)
                        .WithMany()
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ClubCard>()
                        .HasOne(t => t.User)
                        .WithMany()
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserLocation>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.Locations)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSport>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.Sports)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainerCard>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.TrainerCards)
                        .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<ClubCard>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.ClubCards)
                        .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<UserTraining>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.UserTrainings)
                        .HasForeignKey(t => t.UserId);
        }
    }
}
