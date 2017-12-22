using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Raefftec.CatchEmAll.Services;

namespace Raefftec.CatchEmAll
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Jwt";
                    options.DefaultChallengeScheme = "Jwt";
                })
                .AddJwtBearer("Jwt", options =>
                {
                    var jwtSecret = this.configuration.GetSection("Security").GetValue<string>("JwtSecret");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        //ValidAudience = "the audience you want to validate",
                        ValidateIssuer = false,
                        //ValidIssuer = "the isser you want to validate",

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                    };
                });

            services.AddDbContext<DAL.Context>(
                o => o.UseSqlServer(this.configuration.GetConnectionString("CatchEmAllDatabase"),
                q => q.MigrationsAssembly(typeof(DAL.Context).GetTypeInfo().Assembly.GetName().Name)));

            services.AddTransient<SecurityService>();

            services.Configure<SecurityOptions>(this.configuration.GetSection("Security"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
