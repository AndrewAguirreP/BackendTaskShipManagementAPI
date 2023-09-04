using Microsoft.EntityFrameworkCore;

using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly ShipManagementContext _context;
    private ILogger<ShipRepository> _logger;

    public ShipRepository(ShipManagementContext context,
        ILogger<ShipRepository> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Ship>> GetShipsAsync()
    {
        var query = _context.Ships
            .Include(ship => ship.ShipStatuses)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Ship?> GetByIdAsync(int id)
    {
        var query = _context.Ships
            .Include(ship => ship.ShipStatuses)
            .Where(ship => ship.Id == id)
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> CreateShipAsync(Ship ship)
    {
        _context.Ships.Add(ship);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddShipStatus(ShipStatus shipStatus)
    {
        _context.ShipStatuses.Add(shipStatus);
        return await _context.SaveChangesAsync() > 0;
    }
}