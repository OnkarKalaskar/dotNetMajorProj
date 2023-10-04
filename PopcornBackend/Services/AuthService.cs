using Microsoft.IdentityModel.Tokens;
using PopcornBackend.MailHelper;
using PopcornBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PopcornBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PopcornBackend.MailHelper;
using PopcornBackend.PasswordEncryption;

namespace PopcornBackend.Services
{
    public class AuthService : IAuth
    {
        private readonly MajorProjectDbContext _Context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(MajorProjectDbContext context , IConfiguration configuration, IEmailService emailService)
        {
            this._Context = context;
            this._configuration = configuration;
            this._emailService = emailService;
        }

        public bool Register(User user)
        {
                if(!IsUserAlreadyExist(user))
                {
                user.SubscriptionStart = DateTime.Now;
                var subscription = _Context.Subscriptions.FirstOrDefault(Subscription => Subscription.SubscriptionId == user.SubscriptionId);
                int duration = subscription?.Duration ?? 0;
                user.SubscriptionEnd = DateTime.Now.AddDays(duration);
                
                //password masking
                user.Password = ShaEncrypt.EncryptString(user.Password);

                _Context.Users.Add(user);
                //SendMail(user);
                _Context.SaveChanges();
                //sending mail to newly registered 
                
                     return true;

                }
                
                return false;
            
        }
        User? IAuth.Authenticate(UserLogin userLogin)
        {
            var currentUser = _Context.Users.FirstOrDefault(x => x.Email.ToLower() == userLogin.Email.ToLower()
            && x.Password.ToLower() == userLogin.Password.ToLower() && x.Role.ToLower() == userLogin.Role.ToLower());
            if(currentUser != null)
            {
                return currentUser;
            }

            return null;
            
        }

        User? IAuth.AuthenticateCLient(UserLogin userLogin)
        {
            var currentUser = _Context.Users.FirstOrDefault(x => x.Email.ToLower() == userLogin.Email.ToLower()
            && x.Password.ToLower() == userLogin.Password.ToLower() && x.Role.ToLower() == userLogin.Role.ToLower() && x.IsApproved==1);
            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
            
        }


        

        string IAuth.GenerateToken(User user)
        {
            var jwtKey = _configuration["JWT:Key"];
            var securityKey = jwtKey!=null ?  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) : null;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim("email",user.Email),
                new Claim("role",user.Role),
                new Claim("id",user.Id.ToString()),
                new Claim("name",user.Name),
            };
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsUserAlreadyExist(User user)
        {
            var u1 = _Context.Users.Where(u=>u.Email==user.Email).FirstOrDefault();
            if(u1!=null)
            {
                return true;
            }
            return false;
            
        }

        public async void SendMail(User user)
        {
            Mailrequest mailrequest = new Mailrequest();
            mailrequest.ToEmail = user.Email;
            mailrequest.Subject = "Welcome to pocorn box app " + user.Name + " !!!!!";

            if (user.SubscriptionId == 1)
            {
                mailrequest.Body = "Dear user, \n " +
                    "its a pleasure to have you with us, Hope you enjoy your favorite movies, tvshows and songs with popcorn box app." +
                    "As you have choosen free trial subscription so it will expire after 30 days" +
                    "Subscription start date: " + Convert.ToDateTime(user.SubscriptionStart).ToShortDateString() + "\n" +
                    "Subscription End date: " + Convert.ToDateTime(user.SubscriptionEnd).ToShortDateString()  + "\n" +
                    "Regards, \n" +
                    "Popcorn box app";
                
            }
            await _emailService.SendEmailAsync(mailrequest);
        }

        bool IAuth.ApproveClient(long id)
        {
            var user = _Context.Users.Where(u => u.Id == id && u.IsApproved==0).FirstOrDefault();
            if (user!=null)
            {
                user.IsApproved = 1;
                _Context.Update(user);
                _Context.SaveChanges();
                return true;
            }
            return false;
            
        }

        
    }
}

        
