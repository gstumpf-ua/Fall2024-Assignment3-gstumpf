using Fall2024_Assignment3_gstumpf.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fall2024_Assignment3_gstumpf.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key for MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            // Relationships
            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            // Seed data for Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "The Matrix",
                    IMDBLink = "https://www.imdb.com/title/tt0133093/",
                    Genre = "Sci-Fi",
                    Year = 1999,
                    PosterUrl = "https://example.com/matrix.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Inception",
                    IMDBLink = "https://www.imdb.com/title/tt1375666/",
                    Genre = "Sci-Fi",
                    Year = 2010,
                    PosterUrl = "https://example.com/inception.jpg"
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Shawshank Redemption",
                    IMDBLink = "https://www.imdb.com/title/tt0111161/",
                    Genre = "Drama",
                    Year = 1994,
                    PosterUrl = "https://example.com/shawshank.jpg"
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Dark Knight",
                    IMDBLink = "https://www.imdb.com/title/tt0468569/",
                    Genre = "Action",
                    Year = 2006,
                    PosterUrl = "https://example.com/darkknight.jpg"
                },
                new Movie
                {
                    Id = 5,
                    Title = "Forrest Gump",
                    IMDBLink = "https://www.imdb.com/title/tt0109830/",
                    Genre = "Drama",
                    Year = 1994,
                    PosterUrl = "https://example.com/forrestgump.jpg"
                }
            );

            // Seed data for Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Keanu Reeves", Gender = "Male", Age = 60, IMDBLink = "https://www.imdb.com/name/nm0000206/", PhotoUrl = "https://example.com/keanu.jpg" },
                new Actor { Id = 2, Name = "Leonardo DiCaprio", Gender = "Male", Age = 49, IMDBLink = "https://www.imdb.com/name/nm0000138/", PhotoUrl = "https://example.com/leonardo.jpg" },
                new Actor { Id = 3, Name = "Tim Robbins", Gender = "Male", Age = 65, IMDBLink = "https://www.imdb.com/name/nm0000600/", PhotoUrl = "https://example.com/timrobbins.jpg" },
                new Actor { Id = 4, Name = "Christian Bale", Gender = "Male", Age = 50, IMDBLink = "https://www.imdb.com/name/nm0000288/", PhotoUrl = "https://example.com/christianbale.jpg" },
                new Actor { Id = 5, Name = "Tom Hanks", Gender = "Male", Age = 67, IMDBLink = "https://www.imdb.com/name/nm0000158/", PhotoUrl = "https://example.com/tomhanks.jpg" }
            );

            // Seed data for MovieActor relationships
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 1 }, // Keanu Reeves in The Matrix
                new MovieActor { MovieId = 2, ActorId = 2 }, // Leonardo DiCaprio in Inception
                new MovieActor { MovieId = 3, ActorId = 3 }, // Tim Robbins in The Shawshank Redemption
                new MovieActor { MovieId = 4, ActorId = 4 }, // Christian Bale in The Dark Knight
                new MovieActor { MovieId = 5, ActorId = 5 }  // Tom Hanks in Forrest Gump
            );
        }
    }
}
