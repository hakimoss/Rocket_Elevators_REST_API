using Microsoft.EntityFrameworkCore;
using FactInterventionApi.Models;

namespace FactInterventionApi.Models
{
    public class FactInterventionContext : DbContext
    {
        public FactInterventionContext(DbContextOptions<FactInterventionContext> options)
            : base(options)
        {
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
               
                modelBuilder.Entity<Building>()
                .HasKey(b => b.Id);

                modelBuilder.Entity<Battery>()
                .HasKey(x => x.Id);

                modelBuilder.Entity<Column>()
                .HasKey(x => x.Id);

                modelBuilder.Entity<Elevator>()
                .HasKey(x => x.Id);

                modelBuilder.Entity<Lead>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Intervention>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Customer>() 
                .HasKey(x => x.Id);

                
                modelBuilder.Entity<Customer>()
                .HasMany(x => x.Buildings)
                .WithOne( y => y.Customer)
                .HasForeignKey(z => z.customer_Id);
                
                modelBuilder.Entity<Building>()
                .HasMany(x => x.Batteries) 
                .WithOne( y => y.Building)
                .HasForeignKey(z => z.building_id);
                
                modelBuilder.Entity<Battery>()
                .HasMany(x => x.Columns)
                .WithOne(y => y.Battery)
                .HasForeignKey(z => z.battery_id);                

                modelBuilder.Entity<Column>() 
                .HasOne(x => x.Battery)
                .WithMany(y => y.Columns)
                .HasForeignKey(z => z.battery_id);

                modelBuilder.Entity<Column>() 
                .HasMany(x => x.Elevators)
                .WithOne(y => y.Column)
                .HasForeignKey(z => z.column_id);

                modelBuilder.Entity<Elevator>() 
                .HasOne(x => x.Column)
                .WithMany(y => y.Elevators)
                .HasForeignKey(z => z.column_id);                
            }

        public DbSet<Building> buildings { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Intervention> interventions { get; set; }

    }
}