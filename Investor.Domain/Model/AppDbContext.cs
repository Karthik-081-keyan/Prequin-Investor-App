using Microsoft.EntityFrameworkCore;

namespace Investor.Domain.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<InvestorModel> Investors { get; set; }
       
    }
}
