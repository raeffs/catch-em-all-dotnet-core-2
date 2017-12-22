using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raefftec.CatchEmAll.DAL;

namespace Raefftec.CatchEmAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly Context context;

        public UserController(DAL.Context context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GetUsers()
        {
            var models = await this.context.Users
                .AsNoTracking()
                .Select(x => new
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email
                })
                .ToListAsync();

            return this.Ok(models);
        }
    }
}
