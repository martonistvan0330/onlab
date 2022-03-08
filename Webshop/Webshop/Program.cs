using Microsoft.AspNetCore;
using Webshop;

CreateWebHostBuilder(args).Build().Run();

static IWebHostBuilder CreateWebHostBuilder(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
                  .UseStartup<Startup>()
                  .UseUrls("http://localhost:5000");
}
