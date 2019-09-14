using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PickAndDropBackEnd.Data;
using PickAndDropBackEnd.Models;

namespace PickAndDropBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<Users> _userManager;
        private SignInManager<Users> _signInManager;
        private readonly ApplicationSettings _appSettings;
//        private readonly AuthenticationDbContext _context;

        public UsersController(UserManager<Users> userManager, SignInManager<Users> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        
//        public UsersController(UserManager<Users> userManager, SignInManager<Users> signInManager, AuthenticationDbContext context)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Users>>> GetComplaintsList()
//        {
//            return await _context.Users.ToListAsync();
//        }
        
        [HttpPost]
        [Route("Register")]
        // api/Users/Register
        public async Task<Object> RegisterUser(UsersModel usersModel)
        {
            var user = new Users()
            {
                UserName = usersModel.UserName,
                Email = usersModel.Email,
                FullName = usersModel.FullName
            };

            try
            {
                var result = _userManager.CreateAsync(user, usersModel.Password);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        //api/Users/Login
        public async Task<IActionResult> Login(LoginUserModel loginUserModel)
        {
            var user = await _userManager.FindByNameAsync(loginUserModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUserModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest("UserName or Password is incorrect");
            }
        }
    }
}