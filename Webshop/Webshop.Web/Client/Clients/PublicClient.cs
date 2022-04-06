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

        public async Task<Category[]?> GetMainCategories()
            => await Client.GetFromJsonAsync<Category[]>("api/category/main");

        public async Task<Category[]?> GetSubCategories(int parentId)
            => await Client.GetFromJsonAsync<Category[]>($"api/category/{parentId}");

        public async Task<Product[]?> GetFilteredProducts(int categoryId)
            => await Client.GetFromJsonAsync<Product[]>($"api/product/filter?categoryId={categoryId}");
    }
}
