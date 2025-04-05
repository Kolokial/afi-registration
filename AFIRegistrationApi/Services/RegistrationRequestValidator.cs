using AFIRegistration.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(user => user.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .Must(BeAValidDate)
                .WithMessage("Date format must be YYYY-MM-DD. Check month and day are in correct order.")
            .Must(BeOver18)
                .WithMessage("You must be over the age of 18 to register.");

        RuleFor(user => user.Email)
            .Must(email => email.EndsWith(".co.uk") || email.EndsWith(".com"))
                .WithMessage("Email address must end with .co.uk or .com")
            .Must(email => email.Split("@")[0].Length >= 4)
                .WithMessage("The local part of your email address must be 4 or more characters long.");
    }

    private bool BeAValidDate(string dob)
    {
        DateTime date;
        return DateTime.TryParse(dob, out date);
    }

    private bool BeOver18(string dob)
    {
        return DateTime.UtcNow.AddYears(-18) > DateTime.Parse(dob);
    }
}