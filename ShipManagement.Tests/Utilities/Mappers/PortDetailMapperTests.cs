using AutoMapper;

using ShipManagement.Common.Mappers;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Tests.Utilities.Mappers;

public class PortDetailMapperTests
{
    [Fact]
    public void CreateMap_ShouldMapPortToPortDetail_Correctly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PortDetailMapper>();
        });
        var mapper = configuration.CreateMapper();

        var port = new Port
        {
            Id = 1,
            Name = "Sample Port",
            Latitude = 38.0,
            Longitude = -75.0
        };

        // Act
        var mappedPortDetail = mapper.Map<PortDetail>(port);

        // Assert
        Assert.Equal(port.Id, mappedPortDetail.Id);
        Assert.Equal(port.Name, mappedPortDetail.Name);
        Assert.NotNull(mappedPortDetail.GeoLocation);
        Assert.Equal(port.Latitude, mappedPortDetail.GeoLocation.Latitude);
        Assert.Equal(port.Longitude, mappedPortDetail.GeoLocation.Longitude);
    }

    [Fact]
    public void CreateMap_ShouldMapPortToPortDetail_WithDifferentData()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PortDetailMapper>();
        });
        var mapper = configuration.CreateMapper();

        var port = new Port
        {
            Id = 2,
            Name = "Another Port",
            Latitude = 40.0,
            Longitude = -70.0
        };

        // Act
        var mappedPortDetail = mapper.Map<PortDetail>(port);

        // Assert
        Assert.Equal(port.Id, mappedPortDetail.Id);
        Assert.Equal(port.Name, mappedPortDetail.Name);
        Assert.NotNull(mappedPortDetail.GeoLocation);
        Assert.Equal(port.Latitude, mappedPortDetail.GeoLocation.Latitude);
        Assert.Equal(port.Longitude, mappedPortDetail.GeoLocation.Longitude);
    }
}