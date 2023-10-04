using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Protocol;
using PopcornBackend.DTO;
using PopcornBackend.Models;
using PopcornBackend.PasswordEncryption;
using PopcornBackend.Services;

namespace PopcornBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IAuth auth,IUserService userService, ILogger<UserController> logger)
        {
            this._auth = auth;
            this._userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public ActionResult Register(User user)
        {
            if (_auth.Register(user))
            {
                _logger.LogInformation("User registered successfully");
                return Ok(new { status = 200, isSuccess = true, message = "REGISTERED SUCCESSFULLY" });
            }
            else
            {
                _logger.LogInformation("User already exist");
                return Ok(new { status = 200, isSuccess = true, message = "ALREADY EXIST" });
            }
               
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login(UserLogin userLogin)
        {
            User? user;
            string token;
            userLogin.Password = ShaEncrypt.EncryptString(userLogin.Password);

            user = _auth.Authenticate(userLogin);
            if (user != null)
            {
                if (user.Role.Equals("Client"))
                {
                    user = _auth.AuthenticateCLient(userLogin);
                    if(user != null) 
                    {
                        token = _auth.GenerateToken(user);
                        _logger.LogInformation("JWT Token generated");
                        return Ok(token.ToJson());
                    }
                    _logger.LogInformation("client is not approved yet");
                    return Ok(new { status = 200, isSuccess = true, message = "NOT APPROVED" });
                }
                else
                {
                    _logger.LogInformation("JWT Token generated");
                    token = _auth.GenerateToken(user);
                       return Ok(token.ToJson());
                }
            }
            _logger.LogInformation("Login attempt with invalid credentials");
            return Ok(new { status = 200, isSuccess = true, message = "INVALID CREDENTIALS" });
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("/getUsers")]
        public ActionResult<List<UserDto>> GetAllUsers() 
        {
            var user = _userService.GetUsers();
            if(user.Count>0)
            {
                _logger.LogInformation("Send all users data");
                return user;
            }
            _logger.LogInformation("NO USER DATA FOUND");
            return BadRequest("NO USER DATA FOUND");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/getClient")]
        public ActionResult<List<ClientDto>> GetAllclients()
        {
            var user = (_userService.GetClients());
            if (user.Count>0)
            {
                _logger.LogInformation("Send all clients data");
                return user;
            }
            _logger.LogInformation("NO CLIENT DATA FOUND");
            return BadRequest("NO CLIENT DATA FOUND");
        }

        [Authorize(Roles = "Client,User,Admin")]
        [HttpGet("/getUser/{id}")]
        public ActionResult<UserDto> getUser(long id)
        {
            var user = _userService.getUser(id);
            if (user.Id!=0)
            {
                _logger.LogInformation($"user with id : {id} found");
                return user;
            }
            _logger.LogInformation($"user with id : {id} not found");
            return BadRequest("NO USER FOUND");
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("/approveClient/{id}")]
        public IActionResult ApproveClient(long id)
        {
            if(_auth.ApproveClient(id))
            {
                _logger.LogInformation($"client with id : {id} approved");
                return Ok(new { status = 200, isSuccess = true, message = "APPROVED" });
            }
            _logger.LogInformation($"client with id : {id} is not approved");
            return Ok(new { status = 200, isSuccess = true, message = "ALREADYAPPROVED" }); ;
        }

        [Authorize(Roles = "User,Client,Admin")]
        [HttpPatch("/updateUser/{id}")]
        public ActionResult UpdateUser(long id, UserDto user)
        {
            var u = _userService.UpdateUser(user, id);
            if(u)
            {
                _logger.LogInformation($"user with id : {id} updated successfully");
                return Ok(new { status = 200, isSuccess = true, message = "UPDATED" });
            }
            _logger.LogInformation($"user with id : {id} updated successfully");
            return Ok(new { status = 200, isSuccess = true, message = "NOT UPDATED" });

        }

        [Authorize(Roles = "User,Client,Admin")]
        [HttpPatch("/updateProfilePic/{id}")]
        public ActionResult UpdateProfilePic(long id , string pic)
        {
            var u = _userService.UpdateProfilePic(pic, id);
            if (u)
            {
                _logger.LogInformation($"profile pic updated of user with id : {id} ");
                return Ok(new { status = 200, isSuccess = true, message = "UPDATED PROFILEPIC" });
            }
            _logger.LogInformation($"profile pic not updated of user with id : {id} ");
            return Ok(new { status = 200, isSuccess = true, message = "NOT UPDATED PROFILEPIC" });
        }


        [AllowAnonymous]
        [HttpPatch("/forgotpassword")]
        public ActionResult ForgotPassword(UserDto userDto)
        {
            userDto.Password = ShaEncrypt.EncryptString(userDto.Password);

            var u = _userService.ForgotPassword(userDto);
            if(u)
            {
                _logger.LogInformation($"password changed successfully");
                return Ok(new { status = 200, isSuccess = true, message = "CHANGED" });
            }
            _logger.LogInformation($"password not changes successfully");
            return Ok(new { status = 200, isSuccess = true, message = "NOTCHANGED" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/deleteUser/{id}")]
        public ActionResult DeleteUser(long id)
        {
            var u = _userService.DeleteUser(id);
            if(u)
            {
                _logger.LogInformation($"user deleted successfully");
                return Ok(new { status = 200, isSuccess = true, message = "DELETED" });
            }
            _logger.LogInformation($"user not deleted successfully");
            return Ok(new { status = 200, isSuccess = true, message = "USER NOT FOUND" });

        }
    }
}
