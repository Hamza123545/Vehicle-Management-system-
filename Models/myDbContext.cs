using Microsoft.EntityFrameworkCore;

namespace VehicleInsurance.Models
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options) : base(options)
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

            // Customer -> Policies (Cascade delete allowed)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Policies)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            // Policy -> Vehicle (Cascade delete allowed)
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Vehicle)
                .WithMany(v => v.Policies)
                .HasForeignKey(p => p.VehicleID)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer -> Billing (NoAction to break cascade path)
            modelBuilder.Entity<Billing>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Billings)
                .HasForeignKey(b => b.CustomerID)
                .OnDelete(DeleteBehavior.NoAction);

            // Policy -> Billing (NoAction to break cascade path)
            modelBuilder.Entity<Billing>()
                .HasOne(b => b.Policy)
                .WithMany(p => p.Billings)
                .HasForeignKey(b => b.PolicyID)
                .OnDelete(DeleteBehavior.NoAction);

            // Vehicle -> Billing (NoAction to break cascade path)
            modelBuilder.Entity<Billing>()
                .HasOne(b => b.Vehicle)
                .WithMany(v => v.Billings)
                .HasForeignKey(b => b.VehicleID)
                .OnDelete(DeleteBehavior.NoAction);

            // Unique constraint on BillNumber in Billing
            modelBuilder.Entity<Billing>()
                .HasIndex(b => b.BillNumber)
                .IsUnique();
        }
    }
}
