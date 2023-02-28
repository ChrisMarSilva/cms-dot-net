using AwesomeDevEvents.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {  }

        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // DevEvent
            builder.Entity<DevEvent>(e =>
            {
                e.HasKey(de => de.Id);

                e.Property(de => de.Title)
                    .IsRequired(false);

                e.Property(de => de.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(de => de.StartDate)
                    .HasColumnName("Start_Date");

                e.Property(de => de.EndDate)
                    .HasColumnName("End_Date");

                e.HasMany(de => de.Speakers)
                    .WithOne()
                    .HasForeignKey(s => s.DevEventId);
            });

            // DevEventSpeaker
            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }
    }
}
