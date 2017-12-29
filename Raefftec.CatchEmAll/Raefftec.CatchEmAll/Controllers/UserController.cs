using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raefftec.CatchEmAll.Models;
using Raefftec.CatchEmAll.Services;

namespace Raefftec.CatchEmAll.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DAL.Context context;
        private readonly SecurityService security;

        public UserController(DAL.Context context, SecurityService security)
        {
            this.context = context;
            this.security = security;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int? page = null)
        {
            var model = await this.context.Users
                .AsNoTracking()
                .Select(x => new User
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    IsAdmin = x.IsAdmin
                })
                .ToPageAsync();

            return this.Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            var model = await this.context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new User
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    IsAdmin = x.IsAdmin
                })
                .SingleOrDefaultAsync();

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User model)
        {
            var entry = this.context.Users.Add(new DAL.User
            {
                Username = model.Username,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                PasswordHash = this.security.CreateHash(model.Username)
            });

            await this.context.SaveChangesAsync();

            return await this.GetUser(entry.Entity.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] User model)
        {
            var entity = await this.context.Users.AsTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return this.NotFound();
            }

            if (entity.Username == this.HttpContext.User.Identity.Name)
            {
                return this.BadRequest();
            }

            entity.IsAdmin = model.IsAdmin;

            await this.context.SaveChangesAsync();

            return await this.GetUser(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            var entity = await this.context.Users.AsTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return this.NotFound();
            }

            if (entity.Username == this.HttpContext.User.Identity.Name)
            {
                return this.BadRequest();
            }

            this.context.Remove(entity);

            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
