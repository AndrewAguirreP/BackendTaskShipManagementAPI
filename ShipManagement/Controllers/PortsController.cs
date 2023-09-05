using Microsoft.AspNetCore.Mvc;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Services.Interfaces;

namespace ShipManagement.Controllers;

[ApiController]
[Route("api/ports")]
public class PortsController : ControllerBase
{
    private readonly IPortService _portService;

    public PortsController(IPortService portService) => _portService = portService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PortDetail>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PortDetail>>> GetPortsAsync()
    {
        var ports = await _portService.GetPortsAsync();

        if (ports.IsNullOrEmpty())
            return NoContent();

        return Ok(ports);
    }
}