using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Repositories.Interfaces;

public interface IPortRepository
{
    Task<IEnumerable<Port>> GetPortsAsync();
}