using System.ComponentModel.DataAnnotations;

namespace PopcornBackend.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
