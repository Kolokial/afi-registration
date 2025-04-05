using System.ComponentModel.DataAnnotations;

namespace AFIRegistration.Requests;

public class RegistrationRequest
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string PolicyNumber { get; set; }

    public string? DateOfBirth { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}