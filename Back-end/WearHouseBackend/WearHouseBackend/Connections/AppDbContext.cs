using Microsoft.EntityFrameworkCore;
using WearHouseBackend.Models.Database;

namespace WearHouseBackend.Connections
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<MsUser> MsUser { get; set; }

        public DbSet<MsCategory> MsCategory { get; set; }

    }
}
