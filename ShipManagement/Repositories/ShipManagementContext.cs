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
    }
}