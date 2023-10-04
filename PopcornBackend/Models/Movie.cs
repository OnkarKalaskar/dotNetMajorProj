namespace PopcornBackend.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string MovieName { get; set;}

        public string MoviePath { get; set;}

        public string MoviePoster { get; set;}

        public string MovieDescription { get; set; }

        public int Likes { get; set; } = 10;

        public long UserId { get; set; }
        public User? User { get; set; }

        //category fk
        public int CategoryId { get; set; }

        public MediaCategory? CategoryRef { get; set; }

        //userMovie Mapping
        public ICollection<UserMovie>? UserMovie { get; set; }
    }
}
