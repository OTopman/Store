using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using BookStoreUser.Dtos;
using BookStoreUser.Helpers;
using BookStoreUser.Models;
using BookStoreUser.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreUser.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository UserRepository;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IConfiguration configuration)
        {
            _logger = logger;
            UserRepository = userRepository;
            this._configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            //Fetch all users
            IEnumerable<User> users = UserRepository.GetUsers();

            //Check if records found
            if (users.Count() <= 0)
            {
                //Send response to client
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new AppResponse
                {
                    Status = "failed",
                    Message = "No record found"
                });
            }

            //Send response to client
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(new AppResponse<IEnumerable<User>>
            {
                Status = "success",
                Data = users,
                Message = "Records retrieved successfully."
            });
        }

        // GET api/v1/user/55555-223afdd
        [HttpGet("{registrationUuid}")]
        public IActionResult Get(string registrationUuid)
        {
            try
            {
                //Fetch single user record
                User user = UserRepository.GetUser(registrationUuid);
                Response.StatusCode = StatusCodes.Status200OK;

                //Send response to client
                return new JsonResult(new AppResponse<User> { Status = "success", Message = "Record retrieved successfully.", Data = user });
            }
            catch (AppException ex)
            {
                //Send error response to client
                Response.StatusCode = ex.GetStatusCode();
                return new JsonResult(new AppResponse { Status = ex.GetStatus() ?? "failed", Message = ex.Message });
            }
            catch (Exception)
            {
                //Send error response to client
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new AppResponse { Status = "failed", Message = "Unknown error occured, please try again." });
            }

        }

        [HttpPost("login"), AllowAnonymous]
        public AppResponse<LoginResponseDto> Login([FromBody] LoginDto loginDto)
        {
            //Fetch single user record
            User user = UserRepository.Login(loginDto);
            Response.StatusCode = StatusCodes.Status200OK;

            //Send response to client
            return new AppResponse<LoginResponseDto>
            {
                Status = "success",
                Message = "Authenticated.",
                Data = new LoginResponseDto
                {
                    AccessToken = new AccessToken
                    {
                        ExpiredAt = DateTime.Now.AddMinutes(30),
                        Token = CreateToken(user)
                    },
                    User = user,
                }
            };
        }

        // POST api/v1/user
        [HttpPost("register"), AllowAnonymous]
        public IActionResult Post([FromForm] AddUserDto req)
        {
            try
            {
                //Encrypt password
                if (req.Password != null)
                {
                    req.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);
                }
                //Save record to database
                User user = UserRepository.AddUser(req);
                Response.StatusCode = StatusCodes.Status200OK;

                //Send response to client
                return new JsonResult(new AppResponse<User> { Status = "success", Message = "Record retrieved successfully.", Data = user });
            }
            catch (AppException ex)
            {
                //Exception handler
                Response.StatusCode = ex.GetStatusCode();
                return new JsonResult(new AppResponse { Status = ex.GetStatus() ?? "failed", Message = ex.Message });
            }
            catch (Exception ex)
            {
                //Exception handler
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new AppResponse { Status = "failed", Message = ex.StackTrace });

                //"Unknown error occured, please try again."
            }
        }

        // PUT api/values/5
        [HttpPut("{registrationUuid}")]
        public IActionResult Put(string registrationUuid, [FromForm] EditUserDto userDto)
        {
            try
            {
                //Edit user's record
                userDto.RegistrationUuid = registrationUuid;
                User user = UserRepository.EditUser(userDto);

                //Send response to client
                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(new AppResponse<User> { Status = "success", Message = "Record edited successfully.", Data = user });
            }
            catch (AppException ex)
            {
                //Exception handler
                Response.StatusCode = ex.GetStatusCode();
                return new JsonResult(new AppResponse { Status = ex.GetStatus() ?? "failed", Message = ex.Message });
            }
            catch (Exception)
            {
                //Exception handler
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new AppResponse { Status = "failed", Message = "Unknown error occured, please try again." });
            }
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.PrimarySid, user.RegistrationUuid),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials, audience: "BookStore", issuer: "http://localhost:5237");

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

