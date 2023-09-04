using AutoMapper;

using Moq;

using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;
using ShipManagement.Services;

namespace ShipManagement.Tests.Services;
public class ShipServiceTests
{
    private readonly Mock<IShipRepository> shipRepositoryMock;
    private readonly Mock<IPortRepository> portRepositoryMock;
    private readonly Mock<IMapper> mapperMock;

    public ShipServiceTests()
    {
        shipRepositoryMock = new Mock<IShipRepository>();
        portRepositoryMock = new Mock<IPortRepository>();
        mapperMock = new Mock<IMapper>();

    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnShipDetail()
    {
        var shipId = 1;
        var ship = new Ship
        {
            Id = shipId,
            Name = "Sample Ship",
            ShipStatuses = new List<ShipStatus>
            {
                new ShipStatus
                {
                    Latitude = 38.0,
                    Longitude = -75.0,
                    Velocity = 12,
                    LastCheckedIn = DateTime.UtcNow.AddHours(-1)
                }
            }
        };
        shipRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(ship);
        mapperMock.Setup(m => m.Map<Ship, ShipDetail>(ship)).Returns(new ShipDetail() { Id = ship.Id });
        var shipService = new ShipService(shipRepositoryMock.Object, portRepositoryMock.Object, mapperMock.Object);
        
        var result = await shipService.GetByIdAsync(shipId);

        Assert.NotNull(result);
        Assert.IsType<ShipDetail>(result);
    }

    [Fact]
    public async Task GetShipsAsync_ShouldReturnShipDetails()
    {
        var ships = new List<Ship>
        {
            new Ship
            {
                Id = 1,
                Name = "Ship 1",
                ShipStatuses = new List<ShipStatus>()
            },
            new Ship
            {
                Id = 2,
                Name = "Ship 2",
                ShipStatuses = new List<ShipStatus>()
            }
        };
        shipRepositoryMock.Setup(r => r.GetShipsAsync()).ReturnsAsync(ships);
        mapperMock.Setup(m => m.Map<IEnumerable<Ship>, IEnumerable<ShipDetail>>(ships)).Returns(new List<ShipDetail>() 
        {
            new ShipDetail
            {
                Id = 1,
                Name = "Ship 1",
            },
            new ShipDetail
            {
                Id = 2,
                Name = "Ship 2",
            }
        });

        var shipService = new ShipService(shipRepositoryMock.Object, portRepositoryMock.Object, mapperMock.Object);

        var result = await shipService.GetShipsAsync();

        Assert.NotNull(result);
        Assert.Equal(ships.Count, result.Count());
    }


    [Fact]
    public async Task GetClosestPortAsync_ClosestPort_ShouldReturnGetClosestPortResponse()
    {
        var shipId = 1;
        var ship = new Ship
        {
            Id = shipId,
            Name = "Sample Ship",
            ShipStatuses = new List<ShipStatus>
            {
                new ShipStatus
                {
                    Latitude = 38.0,
                    Longitude = -75.0,
                    Velocity = 12,
                    LastCheckedIn = DateTime.UtcNow.AddHours(-1)
                }
            }
        };

        var ports = new List<Port>
        {
            new Port
            {
                Id = 1,
                Name = "Port A",
                Latitude = 39.0,
                Longitude = -76.0
            },
            new Port
            {
                Id = 2,
                Name = "Port B",
                Latitude = 37.0,
                Longitude = -74.0
            }
        };

        var closestPort = ports.First();

        shipRepositoryMock.Setup(r => r.GetByIdAsync(shipId)).ReturnsAsync(ship);
        portRepositoryMock.Setup(r => r.GetPortsAsync()).ReturnsAsync(ports);

        mapperMock.Setup(m => m.Map<Ship, ShipDetail>(ship)).Returns(new ShipDetail() { Id = 1, CurrentGeoLocation = new GeoLocation(38.0, -75.0) });
        mapperMock.Setup(m => m.Map<Port, PortDetail>(closestPort)).Returns(new PortDetail() { Id = 2});

        var shipService = new ShipService(shipRepositoryMock.Object, portRepositoryMock.Object, mapperMock.Object);

        var result = await shipService.GetClosestPortAsync(shipId);

        Assert.NotNull(result);
        Assert.NotNull(result.ClosestPort);
        Assert.NotNull(result.TravelInformation);
    }

    [Fact]
    public async Task GetClosestPortAsync_ShipDetail_ShouldReturnGetClosestPortResponse()
    {
        var shipId = 1;
        var ship = new Ship
        {
            Id = shipId,
            Name = "Sample Ship",
            ShipStatuses = new List<ShipStatus>
            {
                new ShipStatus
                {
                    Latitude = 38.0,
                    Longitude = -75.0,
                    Velocity = 12,
                    LastCheckedIn = DateTime.UtcNow.AddHours(-1)
                }
            }
        };

        var ports = new List<Port>
        {
            new Port
            {
                Id = 1,
                Name = "Port A",
                Latitude = 39.0,
                Longitude = -76.0
            },
            new Port
            {
                Id = 2,
                Name = "Port B",
                Latitude = 37.0,
                Longitude = -74.0
            }
        };

        var closestPort = ports.Last();

        shipRepositoryMock.Setup(r => r.GetByIdAsync(shipId)).ReturnsAsync(ship);
        portRepositoryMock.Setup(r => r.GetPortsAsync()).ReturnsAsync(ports);

        mapperMock.Setup(m => m.Map<Ship, ShipDetail>(ship)).Returns(new ShipDetail() { Id = 1, CurrentGeoLocation = new GeoLocation(38.0, -75.0) });
        mapperMock.Setup(m => m.Map<Port, PortDetail>(closestPort)).Returns(new PortDetail() { Id = 2 });
        var shipService = new ShipService(shipRepositoryMock.Object, portRepositoryMock.Object, mapperMock.Object);

        var result = await shipService.GetClosestPortAsync(shipId);

        Assert.NotNull(result);
        Assert.NotNull(result.ShipDetail);
        Assert.NotNull(result.TravelInformation);
    }
}
