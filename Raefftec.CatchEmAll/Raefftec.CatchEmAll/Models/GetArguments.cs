using System;
using System.Linq.Expressions;

namespace Raefftec.CatchEmAll.Models
{
    public class GetArguments<TEntity, TModel>
        where TEntity : class, DAL.IHasIdentifier
    {

        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Expression<Func<TEntity, TModel>> Selector { get; set; }
    }
}
