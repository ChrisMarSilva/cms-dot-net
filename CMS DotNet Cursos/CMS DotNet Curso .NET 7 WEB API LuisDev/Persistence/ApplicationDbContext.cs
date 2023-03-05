using AwesomeDevEvents.API.Models.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {  }

        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }
        // public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.ApplyConfiguration(new DevEventMap());
            // builder.ApplyConfiguration(new DevEventSpeakerMap());
            // builder.ApplyConfiguration(new PacienteMap());
            builder.Ignore<Notification>();
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
