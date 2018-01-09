using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raefftec.CatchEmAll.Models;

namespace Raefftec.CatchEmAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QueryController : BaseController<DAL.Query>
    {
        public QueryController(DAL.Context context)
            : base(context)
        {
            this.DefaultPredicate = x => x.Category.User.Username == this.HttpContext.User.Identity.Name;
        }

        [HttpGet]
        public async Task<IActionResult> GetQueries(int? page = null)
        {
            return await this.GetPageAsync(new GetPageArguments<DAL.Query, QueryOverview>
            {
                Selector = x => new QueryOverview
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name
                }
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQuery([FromRoute] long id)
        {
            return await this.GetAsync(new GetArguments<DAL.Query, Query>
            {
                Predicate = x => x.Id == id,
                Selector = x => new Query
                {
                    Id = x.Id,
                    Name = x.Name
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddQuery([FromBody] Query model)
        {
            return await this.AddAsync(new AddArguments<DAL.Query>
            {
                Entity = new DAL.Query
                {
                    Name = model.Name,
                },
                GetAction = x => this.GetQuery(x)
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateQuery([FromRoute] long id, [FromBody] Query model)
        {
            return await this.UpdateAsync(new UpdateArguments<DAL.Query>
            {
                Predicate = x => x.Id == id,
                UpdateAction = x =>
                {
                    x.Name = model.Name;
                },
                GetAction = x => this.GetQuery(x)
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteQuery([FromRoute] long id)
        {
            return await this.DeleteAsync(new DeleteArguments<DAL.Query>
            {
                Predicate = x => x.Id == id
            });
        }
    }
}
