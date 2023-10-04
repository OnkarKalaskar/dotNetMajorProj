namespace PopcornBackend.Models
{
    public class UserMovie
    {
        public long UserId { get; set; }
        public User? UserRef { get; set; }
        public int MovieId { get; set; }

        public Movie? MovieRef { get; set; }

    }
}
