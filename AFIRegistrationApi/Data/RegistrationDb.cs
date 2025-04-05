using Microsoft.EntityFrameworkCore;
using AFIRegistration.Models;
public class RegistrationDbContext : DbContext
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomerEmail> CustomerEmail { get; set; }
    public DbSet<CustomerDOB> CustomerDOB { get; set; }

    public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
            : base(options)
    { }

}