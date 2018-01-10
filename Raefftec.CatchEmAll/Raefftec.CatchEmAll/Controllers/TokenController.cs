using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Raefftec.CatchEmAll.Models;
using Raefftec.CatchEmAll.Services;

namespace Raefftec.CatchEmAll.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly DAL.Context context;
        private readonly SecurityService security;
        private readonly IOptions<SecurityOptions> options;

        public TokenController(DAL.Context context, SecurityService security, IOptions<SecurityOptions> options)
        {
            this.context = context;
            this.security = security;
            this.options = options;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TokenCreation model)
        {
            var user = await this.context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == model.Username && x.IsEnabled);

            if (user == null)
            {
                this.security.VerifyPassword(model.Password, this.security.CreateHash(model.Password));
            }
            else
            {
                if (this.security.VerifyPassword(model.Password, user.PasswordHash))
                {
                    var token = this.CreateToken(user);
                    return this.Ok(token);
                }
            }

            return BadRequest();
        }

        private string CreateToken(DAL.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var jwtSecret = this.options.Value.JwtSecret;
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)), SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
