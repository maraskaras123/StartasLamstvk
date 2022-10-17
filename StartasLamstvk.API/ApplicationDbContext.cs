using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;

namespace StartasLamstvk.API
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserRacePreference> UserRacePreferences { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RaceOfficial> RaceOfficials { get; set; }
        public DbSet<Wage> Wages { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<LasfCategory> LasfCategories { get; set; }
        public DbSet<LasfCategoryTranslation> LasfCategoryTranslations { get; set; }
        public DbSet<MotoCategory> MotoCategories { get; set; }
        public DbSet<MotoCategoryTranslation> MotoCategoryTranslations { get; set; }
        public DbSet<RaceType> RaceTypes { get; set; }
        public DbSet<RaceTypeTranslation> RaceTypeTranslations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleTranslation> RoleTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Driver);
            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Passenger1);
            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Passenger2);
            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Passenger3);
            modelBuilder.Entity<Crew>()
                .HasOne(x => x.Passenger4);

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Author);
            modelBuilder.Entity<Event>()
                .HasOne(x => x.Manager);

            modelBuilder.Entity<User>()
                .HasMany(x => x.ManagedEvents);
            modelBuilder.Entity<User>()
                .HasMany(x => x.RaceOfficials);

            base.OnModelCreating(modelBuilder);
        }
    }
}