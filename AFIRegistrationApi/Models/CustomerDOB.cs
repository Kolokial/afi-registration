using System.ComponentModel.DataAnnotations;

namespace AFIRegistration.Models;

public class CustomerDOB : IValidatableObject
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateOfBirth != null && DateTime.UtcNow.AddYears(-18) < DateOfBirth)
        {
            yield return new ValidationResult("You must be over 18 in order to register.", [nameof(DateOfBirth)]);
        }

    }
}