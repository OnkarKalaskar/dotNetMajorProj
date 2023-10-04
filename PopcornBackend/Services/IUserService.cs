using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public interface IUserService
    {
        public List<UserDto> GetUsers();
        public List<ClientDto> GetClients();
        public UserDto getUser(long id);
        public bool UpdateUser(UserDto user, long id);
        public bool ForgotPassword(UserDto user);
        public bool DeleteUser(long id);
        public bool UpdateProfilePic(string profilePic, long id);
    }
}
