using ShipManagement.DTOs;

namespace ShipManagement.Services.Interfaces
{
    public interface IShipService
    {
        Task<GetClosestPortResponse?> GetClosestPortAsync(int shipId); 
        Task<IEnumerable<ShipDetail>> GetShipsAsync();

        Task<ShipDetail?> GetByIdAsync(int id);

        Task<bool> CreateShipAsync(CreateShipRequest request);

        Task<bool> PatchShipsVelocityAsync(int id, PatchShipsVelocityRequest request);
    }
}
