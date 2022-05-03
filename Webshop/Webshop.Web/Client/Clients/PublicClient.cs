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

        public async Task<ProductDetails?> GetProduct(int productId)
            => await Client.GetFromJsonAsync<ProductDetails>($"api/product/{productId}");

        public async Task<Category?> GetCategory(int categoryId)
		{
            var response = await Client.GetAsync($"api/category/{categoryId}");
            if (response.IsSuccessStatusCode)
			{
                return await Client.GetFromJsonAsync<Category>($"api/category/{categoryId}");
            }
            return null;
        }

        public async Task<Category[]?> GetMainCategories()
            => await Client.GetFromJsonAsync<Category[]>("api/category/main");

        public async Task<Category[]?> GetSubCategories(int parentId)
            => await Client.GetFromJsonAsync<Category[]>($"api/category/{parentId}/subcategories");

        public async Task<ProductsWithPageCount?> GetFilteredProducts(int categoryId, int page, double minPrice = 1000, double maxPrice = 200000, string sizes = "")
            => await Client.GetFromJsonAsync<ProductsWithPageCount>(
                $"api/product/filter?categoryId={categoryId}&page={page}&minPrice={minPrice}&maxPrice={maxPrice}&sizes={sizes}"
                );
    }
}
