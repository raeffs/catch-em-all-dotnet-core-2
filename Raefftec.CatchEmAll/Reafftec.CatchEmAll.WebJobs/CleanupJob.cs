using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class CleanupJob
    {
        private readonly ContextFactory factory;

        public CleanupJob(ContextFactory factory)
        {
            this.factory = factory;
        }

        public async Task CleanupAsync([TimerTrigger("0 0 * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
        {
            try
            {
                await this.DeleteResultsAsync();
                await this.DeleteQueriesAsync();
                await this.DeleteCategoriesAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to cleanup!");
                throw;
            }
        }

        private async Task DeleteResultsAsync()
        {
            using (var context = this.factory.GetContext())
            {
                var entities = await context.Results.AsTracking()
                    .Where(x => x.Query.IsDeleted || x.Query.Category.IsDeleted)
                    .ToListAsync();

                context.RemoveRange(entities);

                await context.SaveChangesAsync();
            }
        }

        private async Task DeleteQueriesAsync()
        {
            using (var context = this.factory.GetContext())
            {
                var entities = await context.Queries.AsTracking()
                    .Where(x => x.IsDeleted || x.Category.IsDeleted)
                    .ToListAsync();

                context.RemoveRange(entities);

                await context.SaveChangesAsync();
            }
        }

        private async Task DeleteCategoriesAsync()
        {
            using (var context = this.factory.GetContext())
            {
                var entities = await context.Categories.AsTracking()
                    .Where(x => x.IsDeleted)
                    .ToListAsync();

                context.RemoveRange(entities);

                await context.SaveChangesAsync();
            }
        }
    }
}
