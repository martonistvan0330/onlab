﻿using Microsoft.EntityFrameworkCore;
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

            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IAddressInfoRepository, AddressInfoRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductStockRepository, ProductStockRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IShippingInfoRepository, ShippingInfoRepository>();
            services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddTransient<IPaymentInfoRepository, PaymentInfoRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<CategoryManager>();
            services.AddTransient<CustomerManager>();
            services.AddTransient<ProductManager>();
            services.AddTransient<UserManager>();

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
