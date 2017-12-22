using System.Linq;
using System.Threading.Tasks;
using Raefftec.CatchEmAll.Services;

namespace Raefftec.CatchEmAll
{
    internal static class ContextSeed
    {
        public static async Task EnsureSeeded(this DAL.Context context, SecurityService security)
        {
            if (!context.Users.Any())
            {
                await context.Users.AddAsync(new DAL.User
                {
                    Username = "admin",
                    Email = "admin@localhost",
                    PasswordHash = security.CreateHash("unicorn")
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
