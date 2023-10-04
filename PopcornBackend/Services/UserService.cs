using PopcornBackend.DTO;
using PopcornBackend.Models;
using PopcornBackend.Services;

namespace PopcornBackend.Services
{
    public class UserService : IUserService
    {
        private readonly MajorProjectDbContext _context;
        private readonly List<Subscription> _subscriptions;
        public UserService(MajorProjectDbContext context)
        {
            this._context = context;
            this._subscriptions =  context.Subscriptions.ToList();
            
        }
        public List<UserDto> GetUsers()
        {
            List<UserDto> users = new List<UserDto>();
            if (_context!=null)
            {
                foreach (User u in _context.Users.Where(role => role.Role.Equals("User"))) {
                    UserDto userDto = new UserDto();
                    userDto.Id = u.Id;
                    userDto.Name = u.Name;
                    userDto.Email = u.Email;
                    userDto.MobileNo = u.MobileNo;
                    userDto.AlternateMobileNo = u.AlternateMobileNo;
                    userDto.Role = u.Role;
                    userDto.SubscriptionName = _subscriptions.FirstOrDefault(sub => sub.SubscriptionId == u.SubscriptionId)?.PlanName;
                    users.Add(userDto);
                }
            }
            return users;
        }

        public List<ClientDto> GetClients()
        {
            List<ClientDto> clients = new List<ClientDto>();
            if(_context!=null)
            {
                foreach(User u in _context.Users.Where(role => role.Role.Equals("Client")))
                {
                    ClientDto clientDto = new ClientDto();
                    clientDto.Id = u.Id;
                    clientDto.Name = u.Name;
                    clientDto.Email = u.Email;
                    clientDto.Role = u.Role;
                    clientDto.MobileNo = u.MobileNo;
                    clientDto.IsApproved = u.IsApproved;
                    clients.Add(clientDto);
                }
            }
            return clients ;
        }
        

        public UserDto getUser(long id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            UserDto userDto = new UserDto();
            if (user!=null)
            {
                    userDto.Id = user.Id;
                    userDto.Name = user.Name;
                    userDto.Email = user.Email;
                    userDto.Role = user.Role;
                    userDto.MobileNo = user.MobileNo;
                    userDto.ProfilePic = user.ProfilePicture;
                    userDto.AlternateMobileNo = user.AlternateMobileNo;
                    userDto.Password = user.Password;
                    userDto.SubscriptionName = _subscriptions.FirstOrDefault(sub => sub.SubscriptionId == user.SubscriptionId)?.PlanName;
                    
            }
            return userDto;
        }

        public bool UpdateUser(UserDto user,long id)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userToUpdate != null && user !=null)
            {
                if (user.Name != null)
                    userToUpdate.Name = user.Name;
                if (user.MobileNo != null)
                    userToUpdate.MobileNo = user.MobileNo;
                if (user.AlternateMobileNo != null)
                    userToUpdate.AlternateMobileNo = user.AlternateMobileNo;

                _context.Users.Update(userToUpdate);
                _context.SaveChanges();
                return true;
                
            }
            return false;
        }

        public bool ForgotPassword(UserDto user)
        {
            string email="";
            if(user.Email != null)
                email = user.Email;
            
            var u = _context.Users.FirstOrDefault(u=>u.Email.ToLower().Equals(email.ToLower()));
            if (u!=null)
            {
                if(user.Password != null)
                   u.Password = user.Password;

                _context.Users.Update(u);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateProfilePic(string profilePic, long id)
        {
            var userToUpdate = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.ProfilePicture = profilePic;
                _context.Users.Update(userToUpdate);
                _context.SaveChanges();
                return true;

            }
            return false;
        }
        public bool DeleteUser(long id)
        {
            var u = _context.Users.Where(u => u.Id==id).FirstOrDefault();
            if(u!=null)
            {
                _context.Users.Remove(u); 
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
