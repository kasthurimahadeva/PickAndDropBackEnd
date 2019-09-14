using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PickAndDropBackEnd.Models;

namespace PickAndDropBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        
        private UserManager<Users> _userManager;

        public UserProfileController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet]
        [Authorize]
        // api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }
    }
}