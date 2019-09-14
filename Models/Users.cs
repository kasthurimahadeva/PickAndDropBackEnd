using Microsoft.AspNetCore.Identity;

namespace PickAndDropBackEnd.Models
{
    public class Users: IdentityUser
    {
        public string FullName { get; set; }
    }
}