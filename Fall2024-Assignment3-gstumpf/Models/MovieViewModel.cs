using Fall2024_Assignment3_gstumpf.Models;
using System.Collections.Generic;

namespace Fall2024_Assignment3_gstumpf.Models
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string IMDBLink { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Poster { get; set; }
        public List<Actor> Actors { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public string OverallSentiment { get; set; }
    }

    public class ReviewModel
    {
        public string Text { get; set; }
        public string Sentiment { get; set; }
    }
}
