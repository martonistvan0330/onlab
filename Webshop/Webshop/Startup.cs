using Microsoft.EntityFrameworkCore;
using Webshop.BL;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop
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
            services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebshopDb")));

            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<CategoryManager>();

            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
