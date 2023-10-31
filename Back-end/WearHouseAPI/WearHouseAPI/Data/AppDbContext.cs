using Microsoft.EntityFrameworkCore;
using WearHouseAPI.Models.Database;

namespace WearHouseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<WhUser> WhUser { get; set; }

        public DbSet<whCategory> whCategory { get; set; }

    }
}
