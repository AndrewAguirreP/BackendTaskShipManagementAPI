using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Tests.Utilities;

public class ExtensionsTests
{
    [Fact]
    public void ShipToClosestPort_ShouldReturnClosestPort()
    {
        // Sample ports with their coordinates
        Port[] ports = new Port[]
        {
                    new Port { Name = "Port A", Latitude = 40.0, Longitude = -70.0 },
                    new Port { Name = "Port B", Latitude = 34.0, Longitude = -120.0 },
                    new Port { Name = "Port C", Latitude = 45.0, Longitude = -80.0 },
        };

        // Create a ship instance with its coordinates
        Ship ship = new Ship
        {
            Id = 1,
            Name = "Ship 1",
            UniqueShipId = "SHIP-REG-NO-1",
            ShipStatuses = new List<ShipStatus>()
                {
                    new ShipStatus
                    {
                        ShipId = 1,
                        Velocity = 0,
                        Latitude = 38.0,
                        Longitude = -75.0,
                        LastCheckedIn = DateTime.Now
                    }
                }
        };

        // Find the nearest port
        Port nearestPort = ship.ToClosestPort(ports) ?? new Port();

        // Assert that the nearest port is "Port A"
        Assert.Equal("Port A", nearestPort.Name);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrueForNullCollection()
    {
        // Arrange
        var collection = new List<string>();

        // Act
        bool result = collection.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrueForEmptyCollection()
    {
        // Arrange
        IEnumerable<string> collection = Enumerable.Empty<string>();

        // Act
        bool result = collection.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetTravelInformation_ShouldCalculateTravelInformation()
    {
        // Arrange
        GeoLocation currentGeoLocation = new GeoLocation(36.778259, -119.417931);
        GeoLocation targetGeoLocation = new GeoLocation(41.50964, -82.95311);
        double velocityKnots = 10.0;

        // Act
        TravelInformation travelInformation = currentGeoLocation.GetTravelInformation(targetGeoLocation, velocityKnots);

        // Assert
        Assert.Equal("NauticalMiles", travelInformation.Units);
        Assert.Equal(Math.Round(1708.63930885529, 0), Math.Round(travelInformation.Distance, 0));
        Assert.Equal($"7 day(s), 2 hour(s), 51 minute(s)", travelInformation.TravelTime);
        Assert.NotEqual(DateTime.MinValue, travelInformation.EstimatedArrivalTimeUTC);
        Assert.Equal("10 knots", travelInformation.CurrentVelocity);
    }

    [Fact]
    public void GetDistanceInNauticalMiles_ShouldCalculateDistance()
    {
        // Arrange
        GeoLocation currentGeoLocation = new GeoLocation(36.778259, -119.417931);
        GeoLocation targetGeoLocation = new GeoLocation(41.50964, -82.95311);

        // Act
        double distance = currentGeoLocation.GetDistanceInNauticalMiles(targetGeoLocation);

        // Assert
        Assert.Equal(Math.Round(1708.63930885529, 0), Math.Round(distance, 0));
    }

    [Fact]
    public void ToGeoLocation_ShouldConvertPortToGeoLocation()
    {
        // Arrange
        Port port = new Port { Latitude = 40.0, Longitude = -70.0 };

        // Act
        GeoLocation geoLocation = port.ToGeoLocation();

        // Assert
        Assert.Equal(40.0, geoLocation.Latitude);
        Assert.Equal(-70.0, geoLocation.Longitude);
    }

    [Fact]
    public void GetLatestStatus_ShouldReturnNullForNullShip()
    {
        // Arrange
        var ship = new Ship();

        // Act
        var latestStatus = ship?.GetLatestStatus();

        // Assert
        Assert.Null(latestStatus);
    }

    [Fact]
    public void ToClosestPort_ShouldReturnNullForNullStatus()
    {
        // Arrange
        var ship = new Ship { ShipStatuses = new List<ShipStatus>() };
        IEnumerable<Port> ports = new List<Port>();

        // Act
        var closestPort = ship.ToClosestPort(ports);

        // Assert
        Assert.Null(closestPort);
    }
}