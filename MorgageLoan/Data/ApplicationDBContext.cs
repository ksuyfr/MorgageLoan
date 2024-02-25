using Microsoft.EntityFrameworkCore;
using MorgageLoan.Models;

namespace MorgageLoan.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Credit> Credit { get; set; }
    }
}
