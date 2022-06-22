using System.Linq.Expressions;
using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeApp.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Humidity> Humidities { get; set; }
        public DbSet<Co2> Co2s { get; set; }
        public DbSet<Battery> Batteries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataPoint>()
                .Property("Discriminator");


            base.OnModelCreating(modelBuilder);
        }
    }
}