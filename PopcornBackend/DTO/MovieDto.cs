namespace PopcornBackend.DTO
{
    public class MovieDto
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public string MoviePath { get; set; }

        public string MoviePoster { get; set; }

        public string MovieDescription { get; set; }


        public int Likes { get; set; }

        //user id 
        public long UserId { get; set; }

        //category fk
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
    }
}
