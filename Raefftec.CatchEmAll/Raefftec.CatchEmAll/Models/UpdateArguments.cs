using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Raefftec.CatchEmAll.Models
{
    public class UpdateArguments<TEntity>
        where TEntity : class, DAL.IHasIdentifier
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Action<TEntity> UpdateAction { get; set; }

        public Func<long, Task<IActionResult>> GetAction { get; set; }
    }
}
