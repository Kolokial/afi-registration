
using AFIRegistration.Models;
using AFIRegistration.Requests;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly RegistrationDbContext _registrationDb;
    public CustomerController(ILogger<CustomerController> logger, RegistrationDbContext registrationDb)
    {
        _logger = logger;
        _registrationDb = registrationDb;
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] RegistrationRequest user)
    {
        var record = new Customer()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PolicyNumber = user.PolicyNumber,
        };

        if (user.Email != null)
        {
            record.Email = new CustomerEmail()
            {
                Email = user.Email,
            };
        }

        if (user.DateOfBirth != null)
        {
            var dto = new DateTimeOffset(user.DateOfBirth.Value);
            record.DOB = new CustomerDOB() { DateOfBirth = user.DateOfBirth.Value };
        }
        var result = _registrationDb.Customer.Add(record);


        await _registrationDb.SaveChangesAsync();
        return Results.Created($"/customer/{result.Entity.Id}", result.Entity.Id);
    }

    [HttpGet]
    public async Task<OkObjectResult> Get()
    {
        return Ok(_registrationDb.Customer);
    }
}