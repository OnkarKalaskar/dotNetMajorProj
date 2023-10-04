namespace PopcornBackend.Models
{
    public class SongSinger
    {
        public int SongId { get; set; }
        public Song? SongRef { get; set; }
        public int SingerId { get; set; }
        public Singer? SingerRef { get; set; }
    }
}
