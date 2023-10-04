namespace PopcornBackend.Models
{
    public class TvShow
    {
        public int ShowId { get; set; }
        public string ShowName { get; set; }

        public string ShowDescription { get; set; }

        public string ShowPoster { get; set; }

        public string ShowPath { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        //category ref
        public int CategoryId { get; set; }

        public MediaCategory? CategoryRef { get; set; }

        public int Likes { get; set; } = 0;

        public ICollection<UserTvShow>? UserTvShow { get; set; }
    }
}
