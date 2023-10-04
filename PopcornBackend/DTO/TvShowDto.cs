namespace PopcornBackend.DTO
{
    public class TvShowDto
    {
        public long TvShowId { get; set; }
        public string? TvShowName { get; set; }
        public string? TvShowDescription { get; set; }
        public string? TvShowPoster { get;set; }
        public string? TvShowPath { get; set; }
        public string? CategoryName { get; set; }
        public int? Likes { get; set; }
        public long UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
