using System;
using System.Linq;
using Customer.Inquiries.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Inquiries.DataAccess.Base
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Customer>()
                .HasMany(x => x.Transactions)
                .WithOne(x => x.Customer);

            modelBuilder.Entity<Models.Customer>()
                .HasIndex(x => x.ContactEmail)
                .IsUnique();

            modelBuilder.Entity<Models.Customer>()
                .HasIndex(x => x.MobileNumber)
                .IsUnique();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                // and modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }

        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
