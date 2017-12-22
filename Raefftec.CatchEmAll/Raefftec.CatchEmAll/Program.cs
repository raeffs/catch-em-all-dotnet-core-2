using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raefftec.CatchEmAll.Services;

namespace Raefftec.CatchEmAll
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    logger.LogInformation("Attempt to migrate database...");

                    var security = services.GetRequiredService<SecurityService>();
                    using (var context = services.GetRequiredService<DAL.Context>())
                    {
                        await context.Database.MigrateAsync().ConfigureAwait(false);
                        await context.EnsureSeeded(security);
                    }
                }
                catch (Exception exception)
                {
                    logger.LogCritical(exception, "Failed to migrate database!");
                }
            }

            await host.RunAsync();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
