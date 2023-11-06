using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CLIappDT.Models;

class ApplicationContext : DbContext 
{
    public DbSet<Trip> Trips { get; set; } = null!;

    public ApplicationContext()
    {
        
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // insert your path
        optionsBuilder.UseSqlServer("{your_conn_string}");
    }
}
