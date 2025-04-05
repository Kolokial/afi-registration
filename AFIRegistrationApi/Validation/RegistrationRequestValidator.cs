using AFIRegistration.Requests;
using FluentValidation;

namespace AFIRegistration.Validation;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        When(user => user.DateOfBirth != null, () =>
        {
            RuleFor(user => user.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .Must(BeAValidDate)
                .WithMessage(ErrorMessages.DATE_FORMAT_INVALID)
            .Must(BeOver18)
                .WithMessage(ErrorMessages.MUST_BE_OVER_18);
        });


        When(user => user.Email != null, () =>
        {
            RuleFor(user => user.Email)
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