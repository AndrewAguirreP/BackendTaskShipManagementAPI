using Microsoft.AspNetCore.Mvc;

using ShipManagement.Common;
using ShipManagement.DTOs;
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
    [ProducesResponseType(typeof(IEnumerable<ShipDetail>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ShipDetail>>> GetShips()
    {
        var ships = await _shipService.GetShipsAsync();

        if (ships.IsNullOrEmpty())
            return NoContent();

        return Ok(ships);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ShipDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ShipDetail>> GetShipAsync([FromRoute] int id)
        => await _shipService.GetByIdAsync(id) is ShipDetail ship ? Ok(ship) : NotFound();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateShipAsync([FromBody] CreateShipRequest request)
        => await _shipService.CreateShipAsync(request) ? Created("", "Success") : Conflict();

    [HttpPatch("{id}/velocity")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchShipsVelocityAsync([Required][FromRoute(Name = "id")] int id,
        [FromBody] PatchShipsVelocityRequest request) =>
         await _shipService.PatchShipsVelocityAsync(id, request) ? NoContent() : NotFound();

    [HttpGet("{id}/closest_port")]
    [ProducesResponseType(typeof(ShipDetail), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetClosestPortResponse>> GetClosestPortAsync([FromRoute] int id) =>
        await _shipService.GetClosestPortAsync(id) is GetClosestPortResponse response && response?.ClosestPort?.Id > 0 ? Ok(response) : NotFound();
}