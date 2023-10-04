namespace PopcornBackend.Models
{
    public class Singer
    {
        public int SingerId { get; set; }
        public string SingerName { get; set;}
        public ICollection<SongSinger>? SongSinger { get; set; }
    }
}
