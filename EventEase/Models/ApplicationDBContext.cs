using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventEase.Models // Adjust the namespace to match your project structure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        // Override OnModelCreating to specify table mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base implementation

            modelBuilder.Entity<Event>().ToTable("Event"); // Maps DbSet 'Events' to table 'Event'
            modelBuilder.Entity<Venue>().ToTable("Venue"); // Maps DbSet 'Venues' to table
            modelBuilder.Entity<Booking>().ToTable("Booking"); // Maps DbSet 'Bookings' to table                                               
        }
    }

}
