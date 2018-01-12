using System;
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
                    PasswordHash = security.CreateHash("unicorn"),
                    IsAdmin = true,
                    IsEnabled = true
                });

                await context.SaveChangesAsync();
            }

            if (!context.Subscriptions.Any())
            {
                await context.Subscriptions.AddAsync(new DAL.Subscription
                {
                    Name = "Demo Subscription",
                    IsDefault = true,
                    HighPriorityQuota = 0,
                    NormalPriotiryQuota = 2,
                    LowPriotityQuota = 4
                });
            }

            if (!context.Categories.Any())
            {
                await context.Categories.AddAsync(new DAL.Category
                {
                    Name = "Cat1",
                    Number = 44092,
                    UserId = 1
                });

                await context.SaveChangesAsync();
            }

            if (!context.Queries.Any())
            {
                await context.Queries.AddAsync(new DAL.Query
                {
                    CategoryId = 1,
                    Name = "Test",
                    Updated = DateTimeOffset.Now,
                    WithAllTheseWords = "105",
                    Priority = DAL.Priority.High
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
