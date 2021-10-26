using eshop.API.Security;
using eshop.Data.Context;
using eshop.Data.Repositories;
using eshop.Services;
using eshop.Services.CategoryService;
using eshop.Services.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, EfProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eshop.API", Version = "v1" });
            });
            // ---- Bu basic içindi
            // services.AddAuthentication("Basic")
            // .AddScheme<BasicAuthenticationOption,BasicAuthenticationHandler>("Basic",null);
            //              -----------------       JWT          -------------------
            // JWT Bearer için aşağıdaki kodları kullandık, basic iptal edildi.
            var bearer = Configuration.GetSection("Bearer"); // appsettings.json a ulaştı
            var issuer = bearer["Issuer"];
            var audience = bearer["Audience"];
            var securityKey = bearer["SecurityKey"];
            //Jwt nin nasıl üretileceğini ve oynaylanacağının kurallarını yazdık
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(option =>{
                        option.TokenValidationParameters = new TokenValidationParameters{
                            ValidateActor=true,
                            ValidateIssuer=true,
                            ValidateAudience=true,
                            ValidateIssuerSigningKey=true,
                            ValidIssuer = issuer,
                            ValidAudience = audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                        };
                    });

            //Db için
            var connectionString = Configuration.GetConnectionString("db");
            services.AddDbContext<EshopDbContext>(option => option.UseNpgsql(connectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eshop.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
