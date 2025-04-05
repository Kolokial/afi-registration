using AFIRegistration.Requests;
using FluentValidation;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(user => user.DateOfBirth)
            .Must(dob => BeAValidDate(dob))
                .WithMessage("Date format must be YYYY-MM-DD")
            .Must(dob => DateTime.UtcNow.AddYears(-18) > DateTime.Parse(dob))
                .WithMessage("You must be over the age of 18 to register.");

        RuleFor(user => user.Email)
            .Must(email => email.EndsWith(".co.uk") || email.EndsWith(".com"))
                .WithMessage("Email address must end with .co.uk or .com")
            .Must(email => email.Split("@")[0].Length >= 4)
                .WithMessage("The local part of your email address must be 4 or more characters long.");
    }

    private bool BeAValidDate(string value)
    {
        DateTime date;
        return DateTime.TryParse(value, out date);
    }
}