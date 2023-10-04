namespace PopcornBackend.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string SongName { get; set; }

        public string SongLyrics { get; set; }

        public string SongType { get; set; }

        public string SongGeneration { get; set; }

        public string SongPath { get; set; }

        public string SongPoster { get; set; }

        public string SongDescription { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }

        //category fk
        public int CategoryId { get; set; }

        public MediaCategory? CategoryRef { get; set; }

        //song singer ref
        public ICollection<SongSinger>? SongSinger { get; set; }

        public int Likes { get; set; } = 10;

        //songSinger Mapping
        public ICollection<UserSong>? UserSong { get; set; }
        
    }
}
