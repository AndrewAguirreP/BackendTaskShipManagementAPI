using ShipManagement.DTOs;

namespace ShipManagement.Services.Interfaces
{
    public interface IPortService
    {
        Task<IEnumerable<PortDetail>> GetPortsAsync();
    }
}