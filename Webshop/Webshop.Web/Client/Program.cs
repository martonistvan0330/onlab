using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Webshop.BL;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories;
using Webshop.DAL.Repositories.Interfaces;
using Webshop.Web.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = Webshop; Trusted_Connection = True;"));

builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IAddressInfoRepository, AddressInfoRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartItemRepository, CartItemRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductStockRepository, ProductStockRepository>();
builder.Services.AddTransient<ISessionRepository, SessionRepository>();
builder.Services.AddTransient<IShippingInfoRepository, ShippingInfoRepository>();
builder.Services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
builder.Services.AddTransient<ISizeRepository, SizeRepository>();
//builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<IPaymentInfoRepository, PaymentInfoRepository>();
builder.Services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<CategoryManager>();
builder.Services.AddTransient<CartManager>();
builder.Services.AddTransient<CustomerManager>();
builder.Services.AddTransient<OrderManager>();
builder.Services.AddTransient<ProductManager>();
builder.Services.AddTransient<SessionManager>();
builder.Services.AddTransient<UserManager>();

await builder.Build().RunAsync();
