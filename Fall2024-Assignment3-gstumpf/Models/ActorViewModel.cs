using Fall2024_Assignment3_gstumpf.Models;
using System.Collections.Generic;

namespace Fall2024_Assignment3_gstumpf.Models
{
    public class ActorViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string IMDBLink { get; set; }
        public string Photo { get; set; }
        public List<Movie> Movies { get; set; }
        public List<TweetModel> Tweets { get; set; }
        public string OverallSentiment { get; set; }
    }

    public class TweetModel
    {
        public string Text { get; set; }
        public string Sentiment { get; set; }
    }
}
