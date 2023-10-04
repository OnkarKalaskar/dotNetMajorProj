namespace PopcornBackend.Models
{
    public class UserSong
    {
        public long UserId { get; set; }
        public User? UserRef { get; set; }
        public int SongId { get; set; }

        public Song? SongRef { get; set; }
    }
}
