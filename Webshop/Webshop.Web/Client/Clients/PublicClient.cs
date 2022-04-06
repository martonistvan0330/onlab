using Webshop.Web.Shared.Models;
using System.Net.Http.Json;

namespace Webshop.Web.Client.Clients
{
    public class PublicClient
    {
        public HttpClient Client { get; }

        public PublicClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<Product[]?> GetMainPageProducts()
            => await Client.GetFromJsonAsync<Product[]>("api/product/main");
    }
}
