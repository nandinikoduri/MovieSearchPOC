namespace MovieSearchPOC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public double AverageVote { get; set; }
        public string ReleaseDate { get; set; }
        public string Overview { get; set; }
    }
}