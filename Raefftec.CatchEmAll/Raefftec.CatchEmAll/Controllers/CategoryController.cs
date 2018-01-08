using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raefftec.CatchEmAll.Models;

namespace Raefftec.CatchEmAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly DAL.Context context;

        public CategoryController(DAL.Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int? page = null)
        {
            var model = await this.context.Categories
                .AsNoTracking()
                .Where(x => x.User.Username == this.HttpContext.User.Identity.Name)
                .Select(x => new Category
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name
                })
                .ToPageAsync();

            return this.Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
        {
            var model = await this.context.Categories
                .AsNoTracking()
                .Where(x => x.Id == id && x.User.Username == this.HttpContext.User.Identity.Name)
                .Select(x => new Category
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name
                })
                .SingleOrDefaultAsync();

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category model)
        {
            var entry = this.context.Categories.Add(new DAL.Category
            {
                Name = model.Name,
                Number = model.Number,
                User = await this.context.Users.SingleAsync(x => x.Username == this.HttpContext.User.Identity.Name)
            });

            await this.context.SaveChangesAsync();

            return await this.GetCategory(entry.Entity.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, [FromBody] Category model)
        {
            var entity = await this.context.Categories.AsTracking().SingleOrDefaultAsync(x => x.Id == id && x.User.Username == this.HttpContext.User.Identity.Name);

            if (entity == null)
            {
                return this.NotFound();
            }

            entity.Name = model.Name;
            entity.Number = model.Number;

            await this.context.SaveChangesAsync();

            return await this.GetCategory(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
        {
            var entity = await this.context.Categories.AsTracking().SingleOrDefaultAsync(x => x.Id == id && x.User.Username == this.HttpContext.User.Identity.Name);

            if (entity == null)
            {
                return this.NotFound();
            }

            this.context.Remove(entity);

            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
