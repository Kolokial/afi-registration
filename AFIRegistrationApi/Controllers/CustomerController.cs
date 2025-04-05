
using AFIRegistration.Models;
using AFIRegistration.Requests;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly RegistrationDbContext _registrationDb;
    private IValidator<RegistrationRequest> _validator;

    public CustomerController(RegistrationDbContext registrationDb, IValidator<RegistrationRequest> validator)
    {
        _registrationDb = registrationDb;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] RegistrationRequest user)
    {

        ValidationResult validationResult = await _validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

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
            record.DOB = new CustomerDOB()
            {
                DateOfBirth = DateTime.Parse(user.DateOfBirth)
            };
        }
        var result = _registrationDb.Customer.Add(record);

        await _registrationDb.SaveChangesAsync();
        return Results.Created($"/customer/{result.Entity.Id}", result.Entity.Id);
    }

    [HttpGet]
    public async Task<OkObjectResult> Get()
    {
        return Ok(_registrationDb.Customer.Include(x => x.DOB).Include(x => x.Email));
    }
}