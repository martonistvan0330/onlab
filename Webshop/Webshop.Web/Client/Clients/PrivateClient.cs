using Webshop.Web.Shared.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Webshop.Web.Client.Clients
{
    public class PrivateClient
    {
        public HttpClient Client { get; }

        public PrivateClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<Product[]?> GetProducts()
        {
            return await Client.GetFromJsonAsync<Product[]>("api/product/main");
        }
    }
}
