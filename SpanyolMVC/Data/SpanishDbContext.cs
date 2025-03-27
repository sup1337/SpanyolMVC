using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Data;

public class SpanishDbContext : DbContext
{
    public SpanishDbContext(DbContextOptions<SpanishDbContext> options) : base(options)
    {
    }

    public DbSet<Words> Words { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuizResult>().HasKey(q => q.Id);
    }
}