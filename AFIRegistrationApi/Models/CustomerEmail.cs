using System.ComponentModel.DataAnnotations;

namespace AFIRegistration.Models;

public class CustomerEmail : IValidatableObject
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public required string Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Email != null)
        {
            var parts = Email.Split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (localPart == null)
            {
                yield return new ValidationResult("Either Date of Birth or Email must be provided.",
                               [nameof(Email)]);
            }

        }
    }
}