namespace PopcornBackend.Models
{
    public class ClientMediaType
    {
        public long ClientId { get; set; }

        public User? UserRef { get; set; }

        public int MediaId { get; set; }

        public MediaType? MediaTypeRef { get; set; }
    }
}
