using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reafftec.CatchEmAll.WebJobs.Helper;
using Reafftec.CatchEmAll.WebJobs.Models;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class UpdateQueriesJob
    {
        private readonly ContextFactory factory;

        public UpdateQueriesJob(ContextFactory factory)
        {
            this.factory = factory;
        }

        [Singleton]
        public async Task UpdateQueryWithHighPriorityAsync([TimerTrigger("5,10,20,25,35,40,50,55 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
        {
            try
            {
                await this.UpdateQueryAsync(Raefftec.CatchEmAll.DAL.Priority.High);
            }
            catch (Exception exception)
            {
                // log and write to db/store
            }
        }

        public async Task UpdateQueryWithNormalPriorityAsync([TimerTrigger("15,30,45 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
        {
            try
            {
                await this.UpdateQueryAsync(Raefftec.CatchEmAll.DAL.Priority.Normal);
            }
            catch (Exception exception)
            {
                // log and write to db/store
            }
        }

        public async Task UpdateQueryWithLowPriorityAsync([TimerTrigger("0 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
        {
            try
            {
                await this.UpdateQueryAsync(Raefftec.CatchEmAll.DAL.Priority.Low);
            }
            catch (Exception exception)
            {
                // log and write to db/store
            }
        }

        private async Task UpdateQueryAsync(Raefftec.CatchEmAll.DAL.Priority priority)
        {
            var parameters = await this.LoadQueryParametersAsync(priority);
            if (parameters == null)
            {
                return;
            }

            try
            {
                var query = await Load.QueryFromSourceAsync(parameters);
                await this.UpdateResultsAsync(parameters, query.Results);
                await this.UnloadQueryAsync(parameters);
            }
            catch (Exception exception)
            {
                await this.UnloadQueryAsync(parameters);
                throw;
            }
        }

        private async Task<QueryParameters> LoadQueryParametersAsync(Raefftec.CatchEmAll.DAL.Priority priority)
        {
            using (var context = this.factory.GetContext())
            {
                var entity = await context.Queries.AsTracking()
                    .Include(x => x.Category)
                    .Where(x => !x.IsDeleted && !x.Category.IsDeleted && !x.IsLocked && x.Priority == priority)
                    .OrderBy(x => x.Updated)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return null;
                }

                entity.IsLocked = true;

                await context.SaveChangesAsync();

                return new QueryParameters
                {
                    Id = entity.Id,
                    Category = entity.Category.Number,
                    UseDescription = entity.UseDescription,
                    WithAllTheseWords = entity.WithAllTheseWords,
                    WithExactlyTheseWords = entity.WithExactlyTheseWords,
                    WithNoneOfTheseWords = entity.WithNoneOfTheseWords,
                    WithOneOfTheseWords = entity.WithOneOfTheseWords,
                    AutoFilterDeletedDuplicates = entity.AutoFilterDeletedDuplicates
                };
            }
        }

        private async Task UpdateResultsAsync(QueryParameters parameters, IEnumerable<ResultSummary> results)
        {
            using (var context = this.factory.GetContext())
            {
                var ids = results.Select(x => x.ExternalId).ToList();
                var entities = await context.Results.AsTracking()
                    .Where(x => x.QueryId == parameters.Id && ids.Contains(x.ExternalId))
                    .ToListAsync();

                foreach (var result in results)
                {
                    var entity = entities.SingleOrDefault(x => x.ExternalId == result.ExternalId);

                    if (entity == null)
                    {
                        entity = new Raefftec.CatchEmAll.DAL.Result
                        {
                            QueryId = parameters.Id,
                            ExternalId = result.ExternalId,
                            IsNew = true
                        };

                        context.Add(entity);
                    }

                    entity.Updated = DateTimeOffset.Now;
                    entity.Name = result.Name;
                    entity.Description = result.Description;
                    entity.Ends = result.Ends ?? entity.Ends;
                    entity.BidPrice = result.BidPrice;
                    entity.PurchasePrice = result.PurchasePrice;
                    entity.IsClosed = false;
                    entity.IsSold = false;
                    entity.FinalPrice = null;

                    if (!entity.IsHidden && parameters.AutoFilterDeletedDuplicates)
                    {
                        var hasDeletedDuplicates = await context.Results.AsNoTracking()
                            .AnyAsync(x => x.QueryId == parameters.Id
                                && x.IsHidden
                                && x.Name == entity.Name
                                && x.Description == entity.Description);

                        entity.IsHidden = entity.IsHidden || hasDeletedDuplicates;
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        private async Task UnloadQueryAsync(QueryParameters parameters)
        {
            using (var context = this.factory.GetContext())
            {
                var entity = await context.Queries.AsTracking()
                    .Where(x => x.Id == parameters.Id)
                    .SingleAsync();

                entity.Updated = DateTimeOffset.Now;
                entity.IsLocked = false;

                await context.SaveChangesAsync();
            }
        }
    }
}
