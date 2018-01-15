using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raefftec.CatchEmAll.Models;

namespace Raefftec.CatchEmAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : BaseController<DAL.Category>
    {
        public CategoryController(DAL.Context context)
            : base(context)
        {
            this.DefaultPredicate = x => x.User.Username == this.HttpContext.User.Identity.Name;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int? page = null)
        {
            return await this.GetPageAsync(new GetPageArguments<DAL.Category, Category>
            {
                Selector = x => new Category
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name
                },
                Page = page.Value
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
        {
            return await this.GetAsync(new GetArguments<DAL.Category, Category>
            {
                Predicate = x => x.Id == id,
                Selector = x => new Category
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category model)
        {
            return await this.AddAsync(new AddArguments<DAL.Category>
            {
                Entity = new DAL.Category
                {
                    Name = model.Name,
                    Number = model.Number,
                    User = await this.context.Users.SingleAsync(x => x.Username == this.HttpContext.User.Identity.Name)
                },
                GetAction = x => this.GetCategory(x)
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, [FromBody] Category model)
        {
            return await this.UpdateAsync(new UpdateArguments<DAL.Category>
            {
                Predicate = x => x.Id == id,
                UpdateAction = x =>
                {
                    x.Name = model.Name;
                    x.Number = model.Number;
                },
                GetAction = x => this.GetCategory(x)
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
        {
            return await this.DeleteAsync(new DeleteArguments<DAL.Category>
            {
                Predicate = x => x.Id == id,
                SoftDeleteAction = x =>
                {
                    foreach (var query in x.Queries)
                    {
                        query.IsDeleted = true;
                        foreach (var result in query.Results)
                        {
                            result.IsDeleted = true;
                        }
                    }
                }
            });
        }
    }
}
