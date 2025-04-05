using AFIRegistration.Requests;
using AFIRegistration.Validation;
using FluentValidation.TestHelper;

namespace AFIRegistrationAPITests;

public class RegistrationRequestValidatorTests
{
    private RegistrationRequestValidator validator;

    private void Setup()
    {
        validator = new RegistrationRequestValidator();
    }

    [Fact]
    public void Should_fail_DOB_validation()
    {
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            DateOfBirth = "2020-09-09"
        };
        var result = validator.TestValidate(model);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.MUST_BE_OVER_18);
    }

    [Fact]
    public void Should_pass_DOB_validation()
    {
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            DateOfBirth = "2005-09-09"
        };
        var result = validator.TestValidate(model);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Should_fail_email__tld()
    {
        Setup();

        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggga@me.co.il"
        };
        var result = validator.TestValidate(model);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_TLD);
    }

    [Fact]
    public void Should_fail_email_local()
    {
        Setup();

        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggg@me.co.uk"
        };
        var result = validator.TestValidate(model);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_LOCAL_PART);
    }

    [Fact]
    public void Should_fail_email_local_and_tld()
    {
        Setup();

        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggg@me.co.il"
        };
        var result = validator.TestValidate(model);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_LOCAL_PART);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_TLD);

    }
}