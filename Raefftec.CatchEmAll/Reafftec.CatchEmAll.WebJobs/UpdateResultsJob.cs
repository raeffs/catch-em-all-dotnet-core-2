using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reafftec.CatchEmAll.WebJobs.Helper;
using Reafftec.CatchEmAll.WebJobs.Models;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class UpdateResultsJob
    {
        private readonly ContextFactory factory;

        public UpdateResultsJob(ContextFactory factory)
        {
            this.factory = factory;
        }

        [Singleton]
        public async Task UpdateResultAsync([TimerTrigger("3/5 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
        {
            try
            {
                await this.InternalUpdateResultAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to update result!");
            }
        }

        private async Task InternalUpdateResultAsync()
        {
            var parameters = await this.LoadResultParametersAsync();
            if (parameters == null)
            {
                return;
            }

            try
            {
                var result = await Load.ResultFromSourceAsync(parameters);
                await this.UpdateResultAsync(parameters, result);
                await this.UnloadResultAsync(parameters);
            }
            catch
            {
                await this.UnloadResultAsync(parameters);
                throw;
            }
        }

        private async Task<ResultParameters> LoadResultParametersAsync()
        {
            using (var context = this.factory.GetContext())
            {
                var now = DateTimeOffset.Now;
                var lastUpdatedBefore = now.Add(TimeSpan.FromHours(1 * -1));

                var entity = await context.Results.AsTracking()
                    .Where(x => !x.IsDeleted && !x.Query.IsDeleted && !x.Query.Category.IsDeleted && !x.Query.IsLocked)
                    .Where(x => !x.IsHidden && !x.IsClosed)
                    .Where(x => x.Updated <= lastUpdatedBefore || x.Ends == null || x.Ends <= now)
                    .OrderBy(x => x.Updated)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return null;
                }

                entity.IsLocked = true;

                await context.SaveChangesAsync();

                return new ResultParameters
                {
                    Id = entity.Id,
                    ExternalId = entity.ExternalId
                };
            }
        }

        private async Task UpdateResultAsync(ResultParameters parameters, Result result)
        {
            using (var context = this.factory.GetContext())
            {
                var entity = await context.Results.AsTracking()
                    .Where(x => x.Id == parameters.Id)
                    .SingleAsync();

                try
                {
                    entity.Ends = result.Ends;
                    entity.IsClosed = result.Closed;
                    entity.IsSold = result.Sold;
                    entity.BidPrice = result.BidPrice ?? entity.BidPrice;
                    entity.PurchasePrice = result.PurchasePrice ?? entity.PurchasePrice;
                    entity.FinalPrice = result.FinalPrice ?? entity.FinalPrice;
                }
                catch
                {
                    // todo add log
                    // we assume the result does no longer exist
                    entity.IsClosed = true;
                }

                await context.SaveChangesAsync();
            }
        }

        private async Task UnloadResultAsync(ResultParameters parameters)
        {
            using (var context = this.factory.GetContext())
            {
                var entity = await context.Results.AsTracking()
                    .Where(x => x.Id == parameters.Id)
                    .SingleAsync();

                entity.Updated = DateTimeOffset.Now;
                entity.IsLocked = false;

                await context.SaveChangesAsync();
            }
        }
    }
}
