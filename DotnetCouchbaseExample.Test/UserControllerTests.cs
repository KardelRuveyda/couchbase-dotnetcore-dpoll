using Moq;
using Couchbase.KeyValue;
using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http.HttpResults;

public class UserControllerTests
{
    private readonly UserInfoController _controller;
    private readonly Mock<ICouchbaseService> _mockCouchbaseService;

    public UserControllerTests()
    {
        _mockCouchbaseService = new Mock<ICouchbaseService>();
        _controller = new UserInfoController(_mockCouchbaseService.Object);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedResult_WhenUserIsCreated()
    {
        // Arrange
        var userInfo = new UserInfo { Id = "1", FirstName = "Alice", LastName = "Johnson" };
        _mockCouchbaseService.Setup(s => s.InsertAsync(userInfo.Id, userInfo))
                             .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateUser(userInfo) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = "1";
        var mockGetResult = new Mock<IGetResult>();
        mockGetResult.Setup(r => r.ContentAs<UserInfo>()).Returns((UserInfo)null);

        _mockCouchbaseService.Setup(service => service.GetAsync(userId))
                            .ReturnsAsync(mockGetResult.Object);

        // Act
        var result = await _controller.GetUserById(userId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetUserById_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var userId = "1";
        var userInfo = new UserInfo { Id = userId, FirstName = "Alice", LastName = "Johnson" };

        var mockGetResult = new Mock<IGetResult>();
        mockGetResult.Setup(r => r.ContentAs<UserInfo>()).Returns(userInfo);

        _mockCouchbaseService.Setup(service => service.GetAsync(userId))
                            .ReturnsAsync(mockGetResult.Object);

        // Act
        var result = await _controller.GetUserById(userId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userInfo, result.Value);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var userInfo = new UserInfo { Id = "1", FirstName = "Alice", LastName = "Johnson" };
        _mockCouchbaseService.Setup(s => s.UpsertAsync(userInfo.Id, userInfo))
                             .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateUser(userInfo.Id, userInfo) as NotFoundObjectResult;

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_NotFound_WhenDeleteIsSuccessful()
    {
        // Arrange
        _mockCouchbaseService.Setup(s => s.RemoveAsync("1")).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteUser("1") as OkObjectResult; ;

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task DeleteUser_ReturnsOkResult_WhenDeleteIsSuccessful()
    {
        // Arrange
        var userId = "1";

        var mockGetResult = new Mock<IGetResult>();
        mockGetResult.Setup(r => r.ContentAs<UserInfo>()).Returns(new UserInfo { Id = userId });

        _mockCouchbaseService.Setup(s => s.GetAsync(userId)).ReturnsAsync(mockGetResult.Object);

        _mockCouchbaseService.Setup(s => s.RemoveAsync(userId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteUser(userId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result); 
        Assert.Equal("User deleted successfully.", result.Value); 
    }

}
