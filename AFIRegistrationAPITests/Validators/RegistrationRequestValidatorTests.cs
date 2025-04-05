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
    public void RegistrationRequestValidator_DOBIsUnder18_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            DateOfBirth = "2020-09-09"
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.MUST_BE_OVER_18);
    }

    [Fact]
    public void RegistrationRequestValidator_DOBIsOver18_Succeeds()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            DateOfBirth = "2005-09-09"
        };
        // Act
        var result = validator.TestValidate(model);
        // ASsert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void RegistrationRequestValidator_InvalidEmailTLD_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggga@me.co.il"
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_TLD);
    }

    [Fact]
    public void RegistrationRequestValidator_InvalidEmailLocalPart_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggg@me.co.uk"
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_LOCAL_PART);
    }

    [Fact]
    public void RegistrationRequestValidator_InvalidEmailAndLocalPart_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggg@me.co.il"
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_LOCAL_PART);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_TLD);
    }

    [Fact]
    public void RegistrationRequestValidator_InvalidEmailAndInvalidDOB_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
            Email = "ggg@me.co.il",
            DateOfBirth = "2025-09-11"
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_LOCAL_PART);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.INVALID_TLD);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.MUST_BE_OVER_18);
    }

    [Fact]
    public void RegistrationRequestValidator_MissingDOBAndEmail_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "Gary",
            LastName = "Luck",
            PolicyNumber = "XX-999999",
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.DATE_OR_EMAIL_REQUIRED);
    }


    [Fact]
    public void RegistrationRequestValidator_FirstAndLastNameTooShort_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "G",
            LastName = "L",
            PolicyNumber = "XX-999999",
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void RegistrationRequestValidator_PolicyNumberWrongFormat_FailsWithErrorMessage()
    {
        // Arrange
        Setup();
        var model = new RegistrationRequest
        {
            FirstName = "G",
            LastName = "L",
            PolicyNumber = "XX99999",
        };
        // Act
        var result = validator.TestValidate(model);
        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        result.ShouldHaveValidationErrorFor(x => x.PolicyNumber);
        Assert.Contains(result.Errors, x => x.ErrorMessage == ErrorMessages.POLICY_NUMBER_FORMAT);
    }
}