

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ShipManagement.Repositories;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Tests.Repositories;
public class PortRepositoryTests
{
    private DbContextOptions<ShipManagementContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ShipManagementContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetPortsAsync_ReturnsPorts()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new ShipManagementContext(options);

        context.Ports.Add(new Port { Id = 1, Name = "Port1" });
        context.Ports.Add(new Port { Id = 2, Name = "Port2" });
        context.SaveChanges();

        var repository = new PortRepository(context, new LoggerFactory().CreateLogger<PortRepository>());

        // Act
        var ports = await repository.GetPortsAsync();

        // Assert
        Assert.Equal(2, ports.Count());
    }
}