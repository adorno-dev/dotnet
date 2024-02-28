using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
        {
            if (context == null || context.Movie == null)
                throw new ArgumentNullException("Null RazorPagesMovieContext");

            // Look for any movies.
            if (context.Movie.Any())
                return; // DB has been seeded.

            context.Movie.AddRange([
                new ("When Harry Met Sally", "Romantic Comedy", 7.99M, DateTime.Parse("1989-02-12"), "R"),
                    new ("Ghostbusters", "Comedy", 8.99M, DateTime.Parse("1984-03-13"), "R"),
                    new ("Ghostbusters 2", "Comedy", 9.99M, DateTime.Parse("1986-02-23"), "R"),
                    new ("Rio Bravo", "Western", 3.99M, DateTime.Parse("1959-04-15"), "R"),
                ]);

            context.SaveChanges();
        }
    }
}