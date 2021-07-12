using Microsoft.EntityFrameworkCore;

namespace FactInterventionApi.Models
{
    public class FactInterventionContext : DbContext
    {
        public FactInterventionContext(DbContextOptions<FactInterventionContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> batteries { get; set; }
        public DbSet<Building> buildings { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Lead> leads { get; set; }
    }
}