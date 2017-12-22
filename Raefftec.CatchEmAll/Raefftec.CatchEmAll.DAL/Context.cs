using Microsoft.EntityFrameworkCore;

namespace Raefftec.CatchEmAll.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
