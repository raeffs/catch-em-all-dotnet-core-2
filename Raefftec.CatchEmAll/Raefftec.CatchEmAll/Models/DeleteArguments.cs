using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Raefftec.CatchEmAll.Models
{
    public class DeleteArguments<TEntity>
        where TEntity : class, DAL.IHasIdentifier
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Func<TEntity, IActionResult> Assertion { get; set; }

        public Action<TEntity> SoftDeleteAction { get; set; }

        public bool ForceHardDelete { get; set; }
    }
}
