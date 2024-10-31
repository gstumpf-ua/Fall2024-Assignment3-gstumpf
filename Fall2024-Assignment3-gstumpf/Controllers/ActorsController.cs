using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_gstumpf.Data;
using Fall2024_Assignment3_gstumpf.Models;


namespace Fall2024_Assignment3_gstumpf.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _context.Actors.Include(m => m.MovieActors)
                                              .ThenInclude(ma => ma.Movie)
                                              .FirstOrDefaultAsync(m => m.Id == id);

            // Call your AI API to get tweets and sentiment analysis here
            //var tweets = await GetActorTweets(actor.Name); // Implement this method
            //var sentimentAnalysis = await AnalyzeSentiment(tweets); // Implement this method

            var viewModel = new ActorViewModel
            {
                Name = actor.Name,
                Gender = actor.Gender,
                Age = actor.Age,
                IMDBLink = actor.IMDBLink,
                Photo = actor.PhotoUrl,
                Movies = actor.MovieActors.Select(ma => ma.Movie).ToList(),
                //Tweets = tweets.Select(t => new TweetModel { Text = t.Text, Sentiment = t.Sentiment }).ToList(),
                //OverallSentiment = sentimentAnalysis.OverallSentiment
            };

            return View(viewModel);
        }


        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string gender, int age, string iMDBLink, string photoUrl)
        {
            if (ModelState.IsValid)
            {
                var actor = new Actor
                {
                    Name = name,
                    Gender = gender,
                    Age = age,
                    IMDBLink = iMDBLink,
                    PhotoUrl = photoUrl
                };

                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string gender, int age, string iMDBLink, string photoUrl)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    actor.Name = name;
                    actor.Gender = gender;
                    actor.Age = age;
                    actor.IMDBLink = iMDBLink;
                    actor.PhotoUrl = photoUrl;

                    _context.Entry(actor).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(id))
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

            return View(actor);
        }


        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
