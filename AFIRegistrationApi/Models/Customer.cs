namespace AFIRegistration.Models;

public class Customer
{
    public int Id { get; set; }
    public required string FrstName { get; set; }
    public required string LastName { get; set; }

    public CustomerDOB? DOB { get; set; }
    public CustomerEmail? Email { get; set; }

}