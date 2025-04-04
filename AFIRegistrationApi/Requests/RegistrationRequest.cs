namespace AFIRegistration.Requests;

public class RegistrationRequest
{
    public int Id { get; set; }
    public required string FrstName { get; set; }
    public required string LastName { get; set; }

    public int? DOB { get; set; }
    public string? Email { get; set; }
}