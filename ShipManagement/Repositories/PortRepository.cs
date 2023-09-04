using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Repositories
{
    public class PortRepository : IPortRepository
    {
        private readonly ShipManagementContext _context;
        private ILogger<PortRepository> _logger;

        public PortRepository(ShipManagementContext context, ILogger<PortRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Port>> GetPortsAsync() => await _context.Ports.ToListAsync();
    }
}
