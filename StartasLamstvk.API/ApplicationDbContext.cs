using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;

namespace StartasLamstvk.API
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
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
        public DbSet<RoleTranslation> RoleTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Crew>(entity =>
            {
                entity.HasOne(x => x.Driver);
                entity.HasOne(x => x.Passenger1);
                entity.HasOne(x => x.Passenger2);
                entity.HasOne(x => x.Passenger3);
                entity.HasOne(x => x.Passenger4);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasOne(x => x.Author);
                entity.HasOne(x => x.Manager);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.ManagedEvents);
                entity.HasMany(x => x.RaceOfficials);
                entity.ToTable("Users");
            });

            modelBuilder.Entity<Role>(entity => entity.ToTable("Roles"));
        }
    }
}