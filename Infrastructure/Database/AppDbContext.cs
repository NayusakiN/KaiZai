using Application.Contracts.Data;
using Domain.Categories;
using Domain.Transactions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schemas.Default);
        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.PasswordHash).IsRequired();
        });
        
        // Transaction configuration
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Date).IsRequired();
            
            // Configure owned value object
            entity.OwnsOne(e => e.Money, money =>
            {
                money.Property(m => m.Amount).HasColumnName("Amount").IsRequired();
                money.Property(m => m.Currency).HasColumnName("Currency").IsRequired().HasMaxLength(3);
            });
            
            // Configure relationships
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            //TODO: remove later if not important
            // Add index for common queries
            //entity.HasIndex(e => new { e.UserId, e.Date });
        });
        
        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CategoryType)
                .HasMaxLength(20) 
                .HasConversion<string>() // Store the enum as a string
                .IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            
            entity.OwnsOne(e => e.Icon, icon =>
            {
                icon.Property(m => m.ColorCode).HasColumnName("IconColorCode").IsRequired().HasMaxLength(10);
                icon.Property(m => m.Key).HasColumnName("IconKey").IsRequired().HasMaxLength(25);
            });
            
            // Configure relationships
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            //TODO: remove later if not important
            // Add index for common queries
            //entity.HasIndex(e => new { e.UserId, e.CategoryType });
        });
    }
}