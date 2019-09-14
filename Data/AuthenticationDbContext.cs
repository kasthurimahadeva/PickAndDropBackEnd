using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PickAndDropBackEnd.Models;

namespace PickAndDropBackEnd.Data
{
    public class AuthenticationDbContext: IdentityDbContext
    {
        public AuthenticationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public new DbSet<Users> Users { get; set; }
    }
}