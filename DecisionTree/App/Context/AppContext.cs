using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Context;

public class DecisionTreeDbContext : DbContext
{
    public DbSet<DecisionNode> DecisionNodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Set your database connection string
        optionsBuilder.UseSqlServer("data source=.;initial catalog=decision-db;User Id=sa;Password=2H13okXCMssql;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define entity relationships
        modelBuilder.Entity<DecisionNode>()
            .HasOne(d => d.TrueBranch)
            .WithMany()
            .HasForeignKey(d => d.TrueBranchId);

        modelBuilder.Entity<DecisionNode>()
            .HasOne(d => d.FalseBranch)
            .WithMany()
            .HasForeignKey(d => d.FalseBranchId);
    }
}
