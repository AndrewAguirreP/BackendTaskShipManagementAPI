using System;
using System.Linq;

using AutoMapper;

using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;
using ShipManagement.Utilities.Mappers;

using Xunit;

namespace ShipManagement.Tests.Utilities.Mappers;

public class CreateShipRequestMapperTests
{
    [Fact]
    public void CreateMap_ShouldMapCreateShipRequestToShip_Correctly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateShipRequestMapper>();
        });
        var mapper = configuration.CreateMapper();

        var createShipRequest = new CreateShipRequest
        {
            UniqueShipId = "SHIP123",
            Name = "Sample Ship",
            Velocity = 12,
            Latitude = 41.0,
            Longitude = -70.0
        };

        // Act
        var mappedShip = mapper.Map<Ship>(createShipRequest);

        // Assert
        Assert.Equal(createShipRequest.UniqueShipId, mappedShip.UniqueShipId);
        Assert.Equal(createShipRequest.Name, mappedShip.Name);
        Assert.Single(mappedShip.ShipStatuses);

        var status = mappedShip.ShipStatuses.First();
        Assert.Equal(createShipRequest.Velocity, status.Velocity);
        Assert.Equal(createShipRequest.Latitude, status.Latitude);
        Assert.Equal(createShipRequest.Longitude, status.Longitude);
    }

    [Fact]
    public void CreateMap_ShouldMapCreateShipRequestToShip_WithDifferentData()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateShipRequestMapper>();
        });
        var mapper = configuration.CreateMapper();

        var createShipRequest = new CreateShipRequest
        {
            UniqueShipId = "SHIP456",
            Name = "Another Ship",
            Velocity = 9,
            Latitude = 35.5,
            Longitude = -80.0
        };

        // Act
        var mappedShip = mapper.Map<Ship>(createShipRequest);

        // Assert
        Assert.Equal(createShipRequest.UniqueShipId, mappedShip.UniqueShipId);
        Assert.Equal(createShipRequest.Name, mappedShip.Name);
        Assert.Single(mappedShip.ShipStatuses);

        var status = mappedShip.ShipStatuses.First();
        Assert.Equal(createShipRequest.Velocity, status.Velocity);
        Assert.Equal(createShipRequest.Latitude, status.Latitude);
        Assert.Equal(createShipRequest.Longitude, status.Longitude);
    }
}
