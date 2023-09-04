using Microsoft.AspNetCore.Mvc;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Services.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace ShipManagement.Controllers;

[ApiController]
[Route("api/ships")]
public class ShipsController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipsController(IShipService shipService) => _shipService = shipService;
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipDetail>>> GetShips()
    {
        var ships = await _shipService.GetShipsAsync();

        if (ships.IsNullOrEmpty())
            return NoContent();

        return Ok(ships);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipDetail>> GetShipAsync([FromRoute] int id)
        => await _shipService.GetByIdAsync(id) is ShipDetail ship ? Ok(ship) : NotFound();

    [HttpPost]
    public async Task<IActionResult> CreateShipAsync([FromBody] CreateShipRequest request) 
        => await _shipService.CreateShipAsync(request) ? Created("", "Success") : Conflict();
    

    [HttpPatch("{id}/velocity")]
    public async Task<IActionResult> PatchShipsVelocityAsync([Required][FromRoute(Name = "id")] int id,
        [FromBody] PatchShipsVelocityRequest request) =>
         await _shipService.PatchShipsVelocityAsync(id, request) ? NoContent() : NotFound();

    [HttpGet("{id}/closest_port")]
    public async Task<ActionResult<GetClosestPortResponse>> GetClosestPortAsync([FromRoute] int id) =>
        await _shipService.GetClosestPortAsync(id) is GetClosestPortResponse response && response?.ClosestPort?.Id > 0 ? Ok(response) : NotFound();
    
}