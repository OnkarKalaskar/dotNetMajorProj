using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopcornBackend.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string? ProfilePicture { get; set; } = "ProfilePic2.webp";

        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNo { get; set; }

        public string? AlternateMobileNo { get; set; }

        public string Role { get; set; }

        //subscription ref
        public int ?SubscriptionId { get; set; }
        public Subscription ?SubscriptionRef { get; set; }

        public DateTime ?SubscriptionStart { get; set; }

        public DateTime ?SubscriptionEnd { get; set; }  
        public DateTime ?Timestamp { get; set; } = DateTime.UtcNow;

        public int IsApproved { get; set; } = 0;
        //userMovie mapping
        public ICollection<UserMovie> ?UserMovie { get; set; }
        public ICollection<UserTvShow> ?UserTvShow { get; set; }
        public ICollection<UserSong> ?UserSong { get; set; }

        public ICollection<ClientMediaType> ?ClientMediaType { get; set; }

        // User to Movie, Song and TvShow mapping

        public ICollection<Movie> ?Movie { get; set; }

        public ICollection<Song>? Song { get; set; }

        public ICollection<TvShow>? TvShow { get; set; }

    }
}
