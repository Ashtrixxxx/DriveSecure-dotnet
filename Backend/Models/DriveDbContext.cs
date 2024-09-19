using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class DriveDbContext : DbContext
    {

        public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options) { }


        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<UserAddress> UserAddress { get; set; }

        public DbSet<InsurancePolicies> InsurancePolicies { get; set; }

         public DbSet<VehicleDetails> VehicleDetails { get; set; }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }

        public DbSet<SupportDocuments> SupportDocuments { get; set; }

        public DbSet<TempFormData> TempFormData { get; set; }

        public DbSet<AdminDetails> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.UserName)
                .IsUnique(); // Set UserName as unique

            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Set Email as unique

            base.OnModelCreating(modelBuilder);
        }

    }
}
