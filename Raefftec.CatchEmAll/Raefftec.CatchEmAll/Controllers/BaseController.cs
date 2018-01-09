using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raefftec.CatchEmAll.Models;

namespace Raefftec.CatchEmAll.Controllers
{
    public abstract class BaseController<TEntity> : Controller
        where TEntity : class, DAL.IHasIdentifier
    {
        protected readonly DAL.Context context;

        protected Expression<Func<TEntity, bool>> DefaultPredicate { get; set; }

        public BaseController(DAL.Context context)
        {
            this.context = context;
        }

        protected async Task<IActionResult> GetPageAsync<TModel>(GetPageArguments<TEntity, TModel> arguments)
        {
            var query = this.context.Set<TEntity>()
                 .AsNoTracking();

            if (this.DefaultPredicate != null)
            {
                query = query.Where(this.DefaultPredicate);
            }

            if (typeof(DAL.IHasSoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Cast<DAL.IHasSoftDelete>().Where(x => !x.IsDeleted).Cast<TEntity>();
            }

            if (arguments.Predicate != null)
            {
                query = query.Where(arguments.Predicate);
            }

            var pageModel = await query
                .Select(arguments.Selector)
                .ToPageAsync(arguments.Page, arguments.PageSize);

            return this.Ok(pageModel);
        }

        protected async Task<IActionResult> GetAsync<TModel>(GetArguments<TEntity, TModel> arguments)
        {
            var query = this.context.Set<TEntity>()
                 .AsNoTracking();

            if (this.DefaultPredicate != null)
            {
                query = query.Where(this.DefaultPredicate);
            }

            if (typeof(DAL.IHasSoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Cast<DAL.IHasSoftDelete>().Where(x => !x.IsDeleted).Cast<TEntity>();
            }

            if (arguments.Predicate != null)
            {
                query = query.Where(arguments.Predicate);
            }

            var model = await query
                .Select(arguments.Selector)
                .SingleOrDefaultAsync();

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        protected async Task<IActionResult> AddAsync(AddArguments<TEntity> arguments)
        {
            var entry = await this.context.AddAsync(arguments.Entity);

            await this.context.SaveChangesAsync();

            return await arguments.GetAction(entry.Entity.Id);
        }

        protected async Task<IActionResult> UpdateAsync(UpdateArguments<TEntity> arguments)
        {
            var query = this.context.Set<TEntity>()
                 .AsTracking();

            if (this.DefaultPredicate != null)
            {
                query = query.Where(this.DefaultPredicate);
            }

            if (typeof(DAL.IHasSoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Cast<DAL.IHasSoftDelete>().Where(x => !x.IsDeleted).Cast<TEntity>();
            }

            if (arguments.Predicate != null)
            {
                query = query.Where(arguments.Predicate);
            }

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return this.NotFound();
            }

            arguments.UpdateAction?.Invoke(entity);

            await this.context.SaveChangesAsync();

            return await arguments.GetAction(entity.Id);
        }

        protected async Task<IActionResult> DeleteAsync(DeleteArguments<TEntity> arguments)
        {
            var query = this.context.Set<TEntity>()
                 .AsTracking();

            if (this.DefaultPredicate != null)
            {
                query = query.Where(this.DefaultPredicate);
            }

            if (arguments.Predicate != null)
            {
                query = query.Where(arguments.Predicate);
            }

            var entity = await query
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                return this.NotFound();
            }

            var assertionResult = arguments.Assertion?.Invoke(entity);
            if (assertionResult != null)
            {
                return assertionResult;
            }

            if (typeof(DAL.IHasSoftDelete).IsAssignableFrom(typeof(TEntity)) && !arguments.ForceHardDelete)
            {
                arguments.SoftDeleteAction?.Invoke(entity);
                (entity as DAL.IHasSoftDelete).IsDeleted = true;
            }
            else
            {
                context.Remove(entity);
            }

            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
