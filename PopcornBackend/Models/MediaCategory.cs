namespace PopcornBackend.Models
{
    public class MediaCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Movie>? Movies { get;set; }
        public ICollection<TvShow> ?TvShows { get;set;}
        public ICollection<Song>? Songs { get;set; }
    }
}
