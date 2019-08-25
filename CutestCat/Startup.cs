using CutestCat.Business;
using CutestCat.Repositories.Http;
using CutestCat.Repositories.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CutestCat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var section = Configuration.GetSection("ApiConfiguration");
            services.Configure<ApiConfiguration>(section);

            services.AddTransient<ICatBusiness, CatBusiness>();
            services.AddTransient<ICatSqlRepository, CatSqlRepository>();
            services.AddTransient<ICatHttpRepository, CatHttpRepository>();
            
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(section["CutestCatClient"]));
            });
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(options => options.WithOrigins(Configuration.GetSection("ApiConfiguration")["CutestCatClient"]).AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();


        }
    }
}
