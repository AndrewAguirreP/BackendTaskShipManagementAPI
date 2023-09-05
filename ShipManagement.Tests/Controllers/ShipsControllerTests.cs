namespace ShipManagement.Tests.Controllers;

public class ShipsControllerTests
{
    [Fact]
    public async Task GetShips_ReturnsOkResult_WhenServiceReturnsData()
    {
        // Arrange
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.GetShipsAsync())
                   .ReturnsAsync(new List<ShipDetail> { new ShipDetail() { Id = 1 } });

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.GetShips();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ShipDetail>>(okResult.Value);
        Assert.NotEmpty(model);
    }

    [Fact]
    public async Task GetShipAsync_ReturnsOkResult_WhenServiceReturnsData()
    {
        // Arrange
        var shipId = 1;
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.GetByIdAsync(shipId))
                   .ReturnsAsync(new ShipDetail { });

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.GetShipAsync(shipId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<ShipDetail>(okResult.Value);
        Assert.NotNull(model);
    }

    [Fact]
    public async Task CreateShipAsync_ReturnsCreatedResult_WhenServiceCreatesShip()
    {
        // Arrange
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.CreateShipAsync(It.IsAny<CreateShipRequest>()))
                   .ReturnsAsync(true);

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.CreateShipAsync(new CreateShipRequest());

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task CreateShipAsync_ReturnsConflictResult_WhenServiceFailsToCreateShip()
    {
        // Arrange
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.CreateShipAsync(It.IsAny<CreateShipRequest>()))
                   .ReturnsAsync(false);

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.CreateShipAsync(new CreateShipRequest());

        // Assert
        Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public async Task PatchShipsVelocityAsync_ReturnsNoContent_WhenServicePatchesVelocity()
    {
        // Arrange
        var shipId = 1;
        var request = new PatchShipsVelocityRequest();
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.PatchShipsVelocityAsync(shipId, request))
                   .ReturnsAsync(true);

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.PatchShipsVelocityAsync(shipId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PatchShipsVelocityAsync_ReturnsNotFound_WhenServiceFailsToPatchVelocity()
    {
        // Arrange
        var shipId = 1;
        var request = new PatchShipsVelocityRequest();
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.PatchShipsVelocityAsync(shipId, request))
                   .ReturnsAsync(false);

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.PatchShipsVelocityAsync(shipId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetClosestPortAsync_ReturnsOkResult_WhenServiceReturnsData()
    {
        // Arrange
        var shipId = 1;
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.GetClosestPortAsync(shipId))
                   .ReturnsAsync(new GetClosestPortResponse { ClosestPort = new PortDetail() { Id = 1 } });

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.GetClosestPortAsync(shipId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<GetClosestPortResponse>(okResult.Value);
        Assert.NotNull(model);
    }

    [Fact]
    public async Task GetClosestPortAsync_ReturnsNotFound_WhenServiceFailsToFindPort()
    {
        // Arrange
        var shipId = 1;
        var mockService = new Mock<IShipService>();
        mockService.Setup(service => service.GetClosestPortAsync(shipId))
                  .ReturnsAsync((GetClosestPortResponse?)null);

        var controller = new ShipsController(mockService.Object);

        // Act
        var result = await controller.GetClosestPortAsync(shipId);

        // Assert
        Assert.IsType<ActionResult<GetClosestPortResponse>>(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }
}