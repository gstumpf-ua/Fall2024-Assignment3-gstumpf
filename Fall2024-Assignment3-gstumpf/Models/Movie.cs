using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_gstumpf.Models
{
    public class Movie
    { 
            [Key] // Ensure this is marked as the primary key
            public int Id { get; set; }

            [Required] // Add validation attributes as needed
            public string Title { get; set; }

            [Required]
            [Url] // Ensures the IMDBLink is a valid URL
            public string IMDBLink { get; set; }

            [Required]
            public string Genre { get; set; }

            [Range(1888, 2100)] // Assuming movies can only be released between these years
            public int Year { get; set; }

            [Url] // Ensure the PosterUrl is a valid URL
            public string PosterUrl { get; set; }
        


        public ICollection<MovieActor> MovieActors { get; set; } // Navigation property
    }

}
