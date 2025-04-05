namespace AFIRegistration.Models;

public class CustomerEmail
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public required string Email { get; set; }
}