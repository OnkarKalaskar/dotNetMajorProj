namespace PopcornBackend.Models
{
    public class UserTvShow
    {
        public long UserId { get; set; }
        public User? UserRef { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShowRef { get; set; }
    }
}
