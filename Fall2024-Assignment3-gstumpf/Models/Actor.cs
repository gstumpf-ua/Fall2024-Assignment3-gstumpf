namespace Fall2024_Assignment3_gstumpf.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string IMDBLink { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } // Navigation property
    }

}
