using System.Linq;
using System.Threading.Tasks;

namespace Raefftec.CatchEmAll
{
    internal static class ContextSeed
    {
        public static async Task EnsureSeeded(this DAL.Context context)
        {
            if (!context.Users.Any())
            {
                await context.Users.AddAsync(new DAL.User
                {
                    Username = "admin",
                    Email = "admin@localhost",
                    PasswordHash = Helper.CryptoHelper.CreateHash("unicorn")
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
