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

        modelBuilder.Entity<QuizResult>(entity =>
        {
            entity.HasKey(q => q.Id);
        
           
            entity.HasOne(q => q.Word)   // Navigation property
                .WithMany()            // Nincs fordított navigáció
                .HasForeignKey(q => q.WordId) // Foreign key
                .OnDelete(DeleteBehavior.Restrict); // Törlés viselkedése
        });
    }
}