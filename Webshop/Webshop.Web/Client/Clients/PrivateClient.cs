using Webshop.Web.Shared.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Text;

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

        public async Task<string?> AddProductToCart(int productId, int amount, int sizeId, string userId)
        {
            var cartItem = new CartItem()
            {
                ProductId = productId,
                Amount = amount,
                SizeId = sizeId,
            }.ToJson();
            var content = new StringContent(cartItem.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync($"api/cart?userid={userId}", content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
