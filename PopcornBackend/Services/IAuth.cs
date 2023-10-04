using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public interface IAuth
    {
       public  bool Register(User user);
       public User? Authenticate(UserLogin userLogin);
       public string GenerateToken(User user);
       public bool ApproveClient(long id);
       public User? AuthenticateCLient(UserLogin userLogin);



    }
}
