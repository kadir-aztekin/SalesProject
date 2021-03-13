using Business.Abstract;
using Business.Concrete;
using Core.DependcyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });
            services.AddCors();
            //services.AddSingleton<IProductService ,ProductManager>(); //birisi senden ýproductservice isterse onu productmanager ver
            //services.AddSingleton<IProductDal,EfProductDal >(); //birisi ýproduct dal ýsterse efproductdalý ver ona demek 
            ////IoC Container yapýyoruz burda  
            //services.AddSingleton<IOrderService,OrderManager >();
            //services.AddSingleton<IOrderDal, EfOrderDal>();
            //services.AddSingleton<ICustomerService  , CustomerManager>();
            //services.AddSingleton<ICustomerDal, EfCustomerDal>();
            //services.AddSingleton<ICategoryService, CategoryManager>();
            //services.AddSingleton<ICategoryDal, EfCategoryDal>();
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependecyResolvers(new ICoreModule[] {
            new CoreModule()
            
            });
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();  //bir yere girmek ýcýn anahtar

            app.UseAuthorization(); //yetkýsý varmý yokmu 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
