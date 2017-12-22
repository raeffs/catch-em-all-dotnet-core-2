using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Raefftec.CatchEmAll.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly DAL.Context context;

        public TokenController(DAL.Context context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            var user = await this.context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                Helper.CryptoHelper.VerifyPassword(password, Helper.CryptoHelper.CreateHash(password));
            }
            else
            {
                if (Helper.CryptoHelper.VerifyPassword(password, user.PasswordHash))
                {
                    var token = this.CreateToken(user);
                    return this.Ok(token);
                }
            }

            return BadRequest();
        }

        private string CreateToken(DAL.User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var jwtSecret = "MyVerySecurePersonalSecretString";
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)), SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
