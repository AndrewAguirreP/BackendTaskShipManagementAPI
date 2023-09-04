using AutoMapper;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;
using ShipManagement.Utilities.Mappers;

namespace ShipManagement.Tests.Utilities.Mappers
{
    public class ShipDetailMapperTests
    {
        [Fact]
        public void CreateMap_ShouldMapShipToShipDetail_Correctly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShipDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            var ship = new Ship
            {
                UniqueShipId = "SHIP123",
                Name = "Sample Ship",
                ShipStatuses = new[]
                {
                new ShipStatus
                {
                    Latitude = 38.0,
                    Longitude = -75.0,
                    Velocity = 12
                }
            }
            };

            // Act
            var mappedShipDetail = mapper.Map<ShipDetail>(ship);

            // Assert
            Assert.Equal(ship.UniqueShipId, mappedShipDetail.UniqueShipId);
            Assert.Equal(ship.Name, mappedShipDetail.Name);
            Assert.NotNull(mappedShipDetail.CurrentGeoLocation);
            Assert.Equal(ship.ShipStatuses.First().Latitude, mappedShipDetail.CurrentGeoLocation.Latitude);
            Assert.Equal(ship.ShipStatuses.First().Longitude, mappedShipDetail.CurrentGeoLocation.Longitude);
            Assert.Equal(ship.ShipStatuses.First().Velocity, mappedShipDetail.Velocity);

            // You can also add more assertions as needed
        }

        [Fact]
        public void CreateMap_ShouldMapShipToShipDetail_WithDifferentData()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShipDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            var ship = new Ship
            {
                UniqueShipId = "SHIP456",
                Name = "Another Ship",
                ShipStatuses = new[]
                {
                new ShipStatus
                {
                    Latitude = 40.0,
                    Longitude = -70.0,
                    Velocity = 9
                }
            }
            };

            // Act
            var mappedShipDetail = mapper.Map<ShipDetail>(ship);

            // Assert
            Assert.Equal(ship.UniqueShipId, mappedShipDetail.UniqueShipId);
            Assert.Equal(ship.Name, mappedShipDetail.Name);
            Assert.NotNull(mappedShipDetail.CurrentGeoLocation);
            Assert.Equal(ship.ShipStatuses.First().Latitude, mappedShipDetail.CurrentGeoLocation.Latitude);
            Assert.Equal(ship.ShipStatuses.First().Longitude, mappedShipDetail.CurrentGeoLocation.Longitude);
            Assert.Equal(ship.ShipStatuses.First().Velocity, mappedShipDetail.Velocity);

        }
    }
}