using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_gstumpf.Data;
using Fall2024_Assignment3_gstumpf.Models;
using Azure.AI.OpenAI; // Add this using statement for OpenAI
using Microsoft.Extensions.Configuration; // Add this using statement for IConfiguration
using VaderSharp2;
using System.ClientModel; // Add this using statement for VaderSharp2

namespace Fall2024_Assignment3_gstumpf.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AzureOpenAIClient _openAiClient;

        public MoviesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;

            // Fetching secrets from configuration
            string openAiKey = configuration["OpenAI:ApiKey"];
            string targetUri = configuration["TargetUri"];

            // Initialize AzureOpenAIClient with secrets
            _openAiClient = new AzureOpenAIClient(
                new Uri(targetUri),
                new ApiKeyCredential(openAiKey)
            );
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.Include(m => m.MovieActors)
                                              .ThenInclude(ma => ma.Actor)
                                              .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieViewModel
            {
                Title = movie.Title,
                IMDBLink = movie.IMDBLink,
                Genre = movie.Genre,
                Year = movie.Year,
                Poster = movie.PosterUrl,
                Actors = movie.MovieActors.Select(ma => ma.Actor).ToList(),
                // Reviews = reviews.Select(r => new ReviewModel { Text = r.Text, Sentiment = r.Sentiment }).ToList(),
                // OverallSentiment = sentimentAnalysis.OverallSentiment
            };

            return View(viewModel);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string iMDBLink, string genre, int year, string posterUrl)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Validation error: {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Manually create the movie object
                    var movie = new Movie
                    {
                        Title = title,
                        IMDBLink = iMDBLink,
                        Genre = genre,
                        Year = year,
                        PosterUrl = posterUrl
                    };

                    _context.Add(movie);
                    await _context.SaveChangesAsync();

                    // Optional: Check database state after save
                    var moviesInDb = await _context.Movies.ToListAsync();
                    Console.WriteLine($"Movies in DB: {moviesInDb.Count}");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving movie: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                }
            }

            // Recreate the movie object to pass back to the view in case of error
            var errorMovie = new Movie
            {
                Title = title,
                IMDBLink = iMDBLink,
                Genre = genre,
                Year = year,
                PosterUrl = posterUrl
            };

            return View(errorMovie);
        }




        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string title, string iMDBLink, string genre, int year, string posterUrl)
        {
            // Retrieve the movie from the database
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the movie properties
                    movie.Title = title;
                    movie.IMDBLink = iMDBLink;
                    movie.Genre = genre;
                    movie.Year = year;
                    movie.PosterUrl = posterUrl;

                    // Set entity state as modified
                    _context.Entry(movie).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or display it for debugging
                        Console.WriteLine("Error saving movie: " + ex.Message);
                        ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, pass the modified movie data back to the view
            return View(movie);
        }


        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        // Helper method for sentiment analysis (uses VaderSharp2 or similar tool)
        private double GetSentiment(string reviewText)
        {
            var analyzer = new SentimentIntensityAnalyzer();
            var results = analyzer.PolarityScores(reviewText);
            return results.Compound;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}

// Additional Models
public class ReviewModel
{
    public string Text { get; set; }
    public double Sentiment { get; set; }
}

public class SentimentAnalysisResult
{
    public List<ReviewModel> Reviews { get; set; }
    public string OverallSentiment { get; set; }
}
