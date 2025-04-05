using AFIRegistration.Requests;
using AFIRegistration.Validation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AFIRegistrationAPITests;

public class CustomerControllerTests
{
    private CustomerController _controller;
    private void SetUp()
    {
        var options = new DbContextOptionsBuilder<RegistrationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestRegistrationDb" + DateTime.Now.Ticks)
            .Options;
        var validator = new RegistrationRequestValidator();
        var mockDb = new RegistrationDbContext(options);
        _controller = new CustomerController(mockDb, validator);
    }

    [Fact]
    public async Task CustomerController_SendCorrectDataWithDOB_Recieve201()
    {

        // Arrange
        SetUp();
        var user = new RegistrationRequest()
        {
            FirstName = "Garfield",
            LastName = "Look",
            PolicyNumber = "XX-090909",
            DateOfBirth = "2004-12-01"
        };

        // Act
        var result = await _controller.Post(user);

        // Assert
        Assert.IsType<Created<int>>(result);
        Assert.Equal(201, (result as Created<int>).StatusCode);
        Assert.Equal(1, (result as Created<int>).Value);
    }

    [Fact]
    public async Task CustomerController_SendCorrectDataWithEmail_201()
    {
        // Arrange
        SetUp();
        var user = new RegistrationRequest()
        {
            FirstName = "Garfield",
            LastName = "Look",
            PolicyNumber = "XX-090909",
            Email = "gary@me.com"
        };

        // Act
        var result = await _controller.Post(user);

        // Assert
        Assert.IsType<Created<int>>(result);
        Assert.Equal(201, (result as Created<int>).StatusCode);
        Assert.Equal(1, (result as Created<int>).Value);
    }

    [Fact]
    public async Task CustomerController_FirstNameTooShort_Recieve400()
    {

        // Arrange
        SetUp();
        var user = new RegistrationRequest()
        {
            FirstName = "G",
            LastName = "Look",
            PolicyNumber = "XX-090909",
            Email = "gary@me.com"
        };

        // Act
        var result = await _controller.Post(user);

        // Assert
        Assert.IsType<ProblemHttpResult>(result);
        Assert.Equal(400, (result as ProblemHttpResult).StatusCode);
    }

    [Fact]
    public async Task CustomerController_LastNameTooShort_Recieve400()
    {

        // Arrange
        SetUp();
        var user = new RegistrationRequest()
        {
            FirstName = "Gary",
            LastName = "L",
            PolicyNumber = "XX-090909",
            Email = "gary@me.com"
        };

        // Act
        var result = await _controller.Post(user);

        // Assert
        Assert.IsType<ProblemHttpResult>(result);
        Assert.Equal(400, (result as ProblemHttpResult).StatusCode);
    }

    [Fact]
    public async Task CustomerController_PolicyNumberTooShort_Recieve400()
    {

        // Arrange
        SetUp();
        var user = new RegistrationRequest()
        {
            FirstName = "Gary",
            LastName = "L",
            PolicyNumber = "XX-09099",
            Email = "gary@me.com"
        };

        // Act
        var result = await _controller.Post(user);

        // Assert
        Assert.IsType<ProblemHttpResult>(result);
        Assert.Equal(400, (result as ProblemHttpResult).StatusCode);
    }
}
