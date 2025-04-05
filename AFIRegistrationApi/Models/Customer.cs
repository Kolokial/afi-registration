namespace AFIRegistration.Models;

public class Customer
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PolicyNumber { get; set; }

    public CustomerDOB? DOB { get; set; }
    public CustomerEmail? Email { get; set; }

}