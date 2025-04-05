using System.ComponentModel.DataAnnotations;

namespace AFIRegistration.Requests;

public class RegistrationRequest : IValidatableObject
{
    [StringLength(50, MinimumLength = 3)]
    [Required]
    public required string FirstName { get; set; }

    [StringLength(50, MinimumLength = 3)]
    [Required]
    public required string LastName { get; set; }

    [RegularExpression(@"^[A-Z]{2}-[0-9]{6}$")]
    [StringLength(9)]
    [Required]
    public required string PolicyNumber { get; set; }

    [RegularExpression(@"^(19|20)[0-9]{2}-(0|1)[0-9]{1}-(0|1|2|3)[0-9]{1}$"),]
    [StringLength(10)]
    public string? DateOfBirth { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateOfBirth == null && Email == null)
        {
            yield return new ValidationResult("Either Date of Birth or Email must be provided.",
                [nameof(DateOfBirth), nameof(Email)]);
        }
    }
}