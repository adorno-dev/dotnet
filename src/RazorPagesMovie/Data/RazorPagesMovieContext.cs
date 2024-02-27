using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options) {}

        public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
                    .Property(m => m.Price)
                    .HasPrecision(18, 2);
    }
}
