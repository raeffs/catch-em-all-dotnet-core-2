using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Raefftec.CatchEmAll.Models
{
    public class AddArguments<TEntity>
        where TEntity : class, DAL.IHasIdentifier
    {
        public TEntity Entity { get; set; }

        public Func<long, Task<IActionResult>> GetAction { get; set; }
    }
}
