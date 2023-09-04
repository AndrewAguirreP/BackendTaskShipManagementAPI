using Microsoft.EntityFrameworkCore;

using ShipManagement.Repositories.Models;

namespace ShipManagement.Repositories
{
    public class ShipManagementContext : DbContext
    {
        public ShipManagementContext(DbContextOptions<ShipManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Port> Ports { get; set; } = null!;
        public DbSet<Ship> Ships { get; set; } = null!;
        public DbSet<ShipStatus> ShipStatuses { get; set; } = null!;


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Ship>().ToTable("Ships");
        //    modelBuilder.Entity<Ship>().HasKey(ship => ship.Id);
        //    modelBuilder.Entity<Ship>().HasMany(typeof(ShipStatus), "ShipStatuses");

        //    modelBuilder.Entity<ShipStatus>()
        //        .HasOne(shipStatus => shipStatus.Ship)
        //        .WithMany(ship => ship.ShipStatuses)
        //        .HasForeignKey(ss => ss.ShipId);
        //}

  
    }
}