
using Microsoft.EntityFrameworkCore;
using Webshop.BL;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.Web
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
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductStockRepository, ProductStockRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IShippingInfoRepository, ShippingInfoRepository>();
            services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            //services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IPaymentInfoRepository, PaymentInfoRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<CategoryManager>();
            services.AddTransient<CartManager>();
            services.AddTransient<CustomerManager>();
            services.AddTransient<OrderManager>();
            services.AddTransient<ProductManager>();
            services.AddTransient<SessionManager>();
            services.AddTransient<UserManager>();

            services.AddRouting();
            services.AddRazorPages();
            // services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}