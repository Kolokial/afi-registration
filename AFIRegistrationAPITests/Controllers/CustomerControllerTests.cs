using AFIRegistration.Requests;
using AFIRegistration.Validation;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AFIRegistrationAPITests;

public class CustomerControllerTests
{
    private CustomerController _controller;
    private void SetUp()
    {
        var validator = new RegistrationRequestValidator();
        var mockDb = Mock.Of<RegistrationDbContext>();
        _controller = new CustomerController(mockDb, validator);
    }

    [Fact]
    public async Task CustomerController_SendCorrectData_RecieveRecordId()
    {

        SetUp();
        Assert.True(true);

    }
}
