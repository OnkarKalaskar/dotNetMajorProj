using PopcornBackend.Models;

namespace PopcornBackend.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? MobileNo { get; set; }

        public string? ProfilePic { get; set; }
        public string? AlternateMobileNo { get; set; }
        public string? Role { get; set; }
        public string? SubscriptionName { get; set; }


    }
}
