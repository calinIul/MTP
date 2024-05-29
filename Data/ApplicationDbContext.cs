using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementApp.Data
{   
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("TEXT");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .IsRequired()
                .HasColumnType("TEXT");

            modelBuilder.Entity<Product>()
                .Property(p => p.ImagePath)
                .IsRequired()
                .HasColumnType("TEXT");

            base.OnModelCreating(modelBuilder);
        }
    }
}