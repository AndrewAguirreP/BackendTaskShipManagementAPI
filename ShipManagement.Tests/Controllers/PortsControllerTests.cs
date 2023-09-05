using Microsoft.AspNetCore.Mvc;

using Moq;

using ShipManagement.Controllers;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;
using ShipManagement.Services.Interfaces;

namespace ShipManagement.Tests.Controllers;
public class PortsControllerTests
{
    [Fact]
    public async Task GetPorts_ReturnsOkResult_WhenPortsExist()
    {
        // Arrange
        var portServiceMock = new Mock<IPortService>();
        portServiceMock.Setup(service => service.GetPortsAsync())
            .ReturnsAsync(new List<PortDetail> { new PortDetail(){ Id = 1} });
        
        var controller = new PortsController(portServiceMock.Object);

        // Act
        var result = await controller.GetPortsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<PortDetail>>(okResult.Value);
        Assert.NotEmpty(model);
    }

    [Fact]
    public async Task GetPorts_ReturnsNoContentResult_WhenNoPortsExist()
    {
        // Arrange
        var portServiceMock = new Mock<IPortService>();
        portServiceMock.Setup(service => service.GetPortsAsync())
            .ReturnsAsync(new List<PortDetail>());

        var controller = new PortsController(portServiceMock.Object);

        // Act
        var result = await controller.GetPortsAsync();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }
}
