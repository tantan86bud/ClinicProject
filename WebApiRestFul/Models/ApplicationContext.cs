using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WebApiRestFul;


public class ApplicationContext : DbContext
{

    //public DbSet<Visit> Visits { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Visit> Visits { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

   

}
