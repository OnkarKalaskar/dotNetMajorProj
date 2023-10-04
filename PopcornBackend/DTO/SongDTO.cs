using PopcornBackend.Models;

namespace PopcornBackend.DTO
{
    public class SongDTO
    {
        public int SongId { get; set; }

        public string SongName { get; set; }

        public string SongLyrics { get; set; }

        public string SongType { get; set; }

        public string SongGeneration { get; set; }

        public string SongPath { get; set; }

        public string SongPoster { get; set; }

        public string SongDescription { get; set; }

        //category fk
        public int CategoryId { get; set; }

        public int? Likes { get; set; }

        public long UserId { get; set; }

        public List<int>? Singers { get; set; }
    }
}
