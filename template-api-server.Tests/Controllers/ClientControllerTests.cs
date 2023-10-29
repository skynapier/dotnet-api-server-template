using template_api_server.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;


public class ClientControllerTests
{
    private readonly Mock<IClientService> _mockService;
    private readonly ClientController _controller;

    public ClientControllerTests()
    {
        _mockService = new Mock<IClientService>();
        _controller = new ClientController(_mockService.Object);
    }

    //Test Method Naming MethodName_StateUnderTest_ExpectedBehavior
    [Fact]
    public void GetClients_ReturnsClients()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllClients())
            .Returns(new List<Client> { new Client { Name = "TestClient" } });

        // Act
        var result = _controller.GetClients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Client>>(okResult.Value);
        Assert.Equal("TestClient", returnValue[0].Name);
    }
}
