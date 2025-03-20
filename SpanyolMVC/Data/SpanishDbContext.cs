using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Data;

public class SpanishDbContext : DbContext
{
    public SpanishDbContext(DbContextOptions<SpanishDbContext> options) : base(options)
    {
    }

    public DbSet<Words> Words { get; set; }
}