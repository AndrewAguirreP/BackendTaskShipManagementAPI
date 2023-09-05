using AutoMapper;

using Moq;

using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;
using ShipManagement.Services;

namespace ShipManagement.Tests.Services;
public class PortServiceTests
{
    [Fact]
    public async Task GetPortsAsync_ReturnsMappedPorts_WhenPortsExist()
    {
        // Arrange
        var portRepositoryMock = new Mock<IPortRepository>();
        var mapperMock = new Mock<IMapper>();

        var portService = new PortService(portRepositoryMock.Object, mapperMock.Object);

        var samplePort = new Port { Id = 1, Name = "Port A" };
        var expectedPortDetail = new PortDetail { Id = 1, Name = "Port A" };


        portRepositoryMock.Setup(repo => repo.GetPortsAsync())
            .ReturnsAsync(new List<Port> { samplePort });


        mapperMock.Setup(mapper => mapper.Map<IEnumerable<Port>, IEnumerable<PortDetail>>(
            It.IsAny<IEnumerable<Port>>()))
            .Returns((IEnumerable<Port> source) => source.Select(port => new PortDetail
            {
                Id = port.Id,
                Name = port.Name
            }));

        // Act
        var result = await portService.GetPortsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        var port = Assert.Single(result);
        Assert.Equal(expectedPortDetail.Id, port.Id);
        Assert.Equal(expectedPortDetail.Name, port.Name);
    }

    [Fact]
    public async Task GetPortsAsync_ReturnsEmptyList_WhenNoPortsExist()
    {
        // Arrange
        var portRepositoryMock = new Mock<IPortRepository>();
        var mapperMock = new Mock<IMapper>();

        var portService = new PortService(portRepositoryMock.Object, mapperMock.Object);

        portRepositoryMock.Setup(repo => repo.GetPortsAsync())
            .ReturnsAsync(new List<Port>());

        // Act
        var result = await portService.GetPortsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
