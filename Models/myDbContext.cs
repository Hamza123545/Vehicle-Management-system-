using Microsoft.EntityFrameworkCore;

namespace VehicleInsurance.Models
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options) :base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Policies)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Vehicle)
                .WithMany(v => v.Policies)
                .HasForeignKey(p => p.VehicleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Billing>()
                .HasOne(b => b.Policy)
                .WithMany()
                .HasForeignKey(b => b.PolicyID);

            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Policy)
                .WithMany()
                .HasForeignKey(c => c.PolicyID);
        }
    }
}
