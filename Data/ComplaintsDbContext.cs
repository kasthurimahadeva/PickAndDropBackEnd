using Microsoft.EntityFrameworkCore;
using PickAndDropBackEnd.Models;

namespace PickAndDropBackEnd.Data
{
    public class ComplaintsDbContext: DbContext
    {
        public ComplaintsDbContext(DbContextOptions<ComplaintsDbContext> options): base(options)
        {
            
        }

        public DbSet<Complaints> Complaints { get; set; }
    }
}