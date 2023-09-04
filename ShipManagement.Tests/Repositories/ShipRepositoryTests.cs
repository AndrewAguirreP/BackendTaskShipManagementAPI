using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShipManagement.Repositories;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Tests.Repositories;

public class ShipRepositoryTests
{
    private DbContextOptions<ShipManagementContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ShipManagementContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetShipsAsync_ReturnsShips()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new ShipManagementContext(options);

        context.Ships.Add(new Ship { Id = 1, Name = "Ship1" });
        context.Ships.Add(new Ship { Id = 2, Name = "Ship2" });
        context.SaveChanges();

        var repository = new ShipRepository(context, new LoggerFactory().CreateLogger<ShipRepository>());

        // Act
        var ships = await repository.GetShipsAsync();

        // Assert
        Assert.Equal(2, ships.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsShip()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new ShipManagementContext(options);

        context.Ships.Add(new Ship { Id = 1, Name = "Ship1" });
        context.SaveChanges();

        var repository = new ShipRepository(context, new LoggerFactory().CreateLogger<ShipRepository>());

        // Act
        var ship = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(ship);
        Assert.Equal("Ship1", ship.Name);
    }

    [Fact]
    public async Task CreateShipAsync_CreatesShip()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new ShipManagementContext(options);

        var repository = new ShipRepository(context, new LoggerFactory().CreateLogger<ShipRepository>());

        var ship = new Ship { Id = 1, Name = "NewShip" };

        // Act
        var result = await repository.CreateShipAsync(ship);

        // Assert
        Assert.True(result);
        Assert.Equal(1, context.Ships.Count());
        Assert.Equal("NewShip", context.Ships.Single().Name);
    }

    [Fact]
    public async Task AddShipStatus_AddsShipStatus()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new ShipManagementContext(options);

        var repository = new ShipRepository(context, new LoggerFactory().CreateLogger<ShipRepository>());

        var shipStatus = new ShipStatus { Id = 1, ShipId = 1, Velocity = 10, Latitude = 45.0, Longitude = -75.0 };

        // Act
        var result = await repository.AddShipStatus(shipStatus);

        // Assert
        Assert.True(result);
        Assert.Equal(1, context.ShipStatuses.Count());
        Assert.Equal(10, context.ShipStatuses.Single().Velocity);
    }
}