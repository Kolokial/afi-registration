using System.Net;
using AFIRegistration.Requests;
using AFIRegistration.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit.Sdk;

namespace AFIRegistrationAPITests;

public class CustomerControllerTests
{
    private CustomerController _controller;
    private void SetUp()
    {
        var options = new DbContextOptionsBuilder<RegistrationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestRegistrationDb")
            .Options;
        var validator = new RegistrationRequestValidator();
        var mockDb = new RegistrationDbContext(options);
        _controller = new CustomerController(mockDb, validator);
    }

    [Fact]
    public async Task CustomerController_SendCorrectData_RecieveRecordId()
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
        Assert.True(true);
        Assert.IsType<Created>(result);
    }
}
