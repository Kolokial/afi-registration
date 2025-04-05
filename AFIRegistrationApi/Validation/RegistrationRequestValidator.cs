using AFIRegistration.Requests;
using FluentValidation;

namespace AFIRegistration.Validation;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(user => user.DateOfBirth)
            .NotEmpty()
            .WithMessage(ErrorMessages.DATE_OR_EMAIL_REQUIRED)
            .When(user => user.Email == null);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.DATE_OR_EMAIL_REQUIRED)
            .When(user => user.DateOfBirth == null);

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .Length(3, 50)
                .WithMessage(ErrorMessages.FIRST_NAME_LENGTH);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .Length(3, 50)
                .WithMessage(ErrorMessages.LAST_NAME_LENGTH);

        RuleFor(user => user.PolicyNumber)
            .NotEmpty()
            .Matches("^[A-Z]{2}-[0-9]{6}$")
                .WithMessage(ErrorMessages.POLICY_NUMBER_FORMAT);

        When(user => user.DateOfBirth != null, () =>
        {
            RuleFor(user => user.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .Matches("^(19|20)[0-9]{2}-(0|1)[0-9]{1}-(0|1|2|3)[0-9]{1}$")
                .WithMessage(ErrorMessages.DATE_FORMAT_INVALID)
            .Must(BeAValidDate)
                .WithMessage(ErrorMessages.DATE_FORMAT_INVALID)
            .Must(BeOver18)
                .WithMessage(ErrorMessages.MUST_BE_OVER_18);
        });


        When(user => user.Email != null, () =>
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .Must(email => email.EndsWith(".co.uk") || email.EndsWith(".com"))
                    .WithMessage(ErrorMessages.INVALID_TLD)
                .Must(email => email.Split("@")[0].Length >= 4)
                    .WithMessage(ErrorMessages.INVALID_LOCAL_PART);
        });
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