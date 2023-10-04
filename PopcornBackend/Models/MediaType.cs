namespace PopcornBackend.Models
{
    public class MediaType
    {
        public int MediaId { get; set; }

        public string MediaName { get; set; }

        public ICollection<ClientMediaType> ?ClientMediaType { get; set;}
    }
}
