using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Repositories.Interfaces
{
    public interface IShipRepository
    {
        Task<IEnumerable<Ship>> GetShipsAsync();

        Task<Ship?> GetByIdAsync(int id);

        Task<bool> CreateShipAsync(Ship ship);

        Task<bool> AddShipStatus(ShipStatus ship);
    }
}
