using System;
using System.Linq.Expressions;

namespace Raefftec.CatchEmAll.Models
{

    public class GetPageArguments<TEntity, TModel>
        where TEntity : class, DAL.IHasIdentifier
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Expression<Func<TEntity, TModel>> Selector { get; set; }
    }

}
