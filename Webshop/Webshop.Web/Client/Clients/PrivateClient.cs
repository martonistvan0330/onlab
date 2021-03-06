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

        public async Task<bool> IsAdmin(string userId)
        {
            return await Client.GetFromJsonAsync<bool>($"api/user/isadmin?userid={userId}");
        }

        public async Task<Product[]?> GetProducts()
        {
            return await Client.GetFromJsonAsync<Product[]>("api/product/main");
        }

        public async Task AddProductToCart(int productId, int amount, int sizeId, string userId)
        {
            var cartItem = new NewCartItem()
            {
                ProductId = productId,
                Amount = amount,
                SizeId = sizeId,
            }.ToJson();
            var content = new StringContent(cartItem.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync($"api/cart?userid={userId}", content);
        }

        public async Task<CartItem[]?> GetCartItems(string userId)
        {
            return await Client.GetFromJsonAsync<CartItem[]>($"api/cart?userid={userId}");
        }

        public async Task<bool> UpdateCartItem(CartItem cartItem, string userId)
        {
            var jsonObject = new UpdateCartItem()
            {
                Id = cartItem.Id,
                SizeId = cartItem.SizeId,
                Amount = cartItem.Amount,
            }.ToJson();
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PutAsync($"api/cart?userid={userId}", content);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCartItem(int cartItemId, string userId)
        {
            var result = await Client.DeleteAsync($"api/cart/{cartItemId}?userid={userId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<int> AddCustomer(NewCustomer customer, string userId)
        {
            var jsonObject = customer.ToJson();
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync($"api/customer?userid={userId}", content);
            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public async Task<Order[]?> GetOrders(string userId)
        {
            return await Client.GetFromJsonAsync<Order[]>($"api/order?userid={userId}");
        }

        public async Task<(bool, int)> CreateOrder(string userId, int customerId)
        {
            var result = await Client.PostAsync($"api/order?userid={userId}&customerid={customerId}", null);
            if (result.IsSuccessStatusCode)
            {
                return (true, int.Parse(await result.Content.ReadAsStringAsync()));
            }
            else 
            {
                return (false, -1);
            }
        }

        public async Task<bool> CancelOrder(int orderId, string userId)
        {
            var result = await Client.PatchAsync($"api/order/{orderId}/cancel?userid={userId}", null);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<OrderDetails?> GetOrderDetails(int orderId, string userId)
        {
            return await Client.GetFromJsonAsync<OrderDetails>($"api/order/{orderId}?userid={userId}");
        }

        public async Task<int> AddNewProduct(NewProduct product, string userId)
        {
            var jsonObject = product.ToJson();
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync($"api/admin/products/add?userid={userId}", content);
            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public async Task<int> UpdateProduct(NewProduct product, int productId, string userId)
        {
            var jsonObject = product.ToJson();
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var result = await Client.PutAsync($"api/admin/products/{productId}/update?userid={userId}", content);
            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public async Task<bool> AddProductImage(MultipartFormDataContent content, int productId, string userId, bool main = false)
        {
            var result = await Client.PostAsync($"api/admin/products/{productId}/addimage/{main}?userid={userId}", content);
            return result.IsSuccessStatusCode;
        }

        public async Task<ProductDetailsWithSize?> GetProductWithSize(int productId, string userId)
        {
            return await Client.GetFromJsonAsync<ProductDetailsWithSize>($"api/admin/products/{productId}?userid={userId}");
        }
    }
}
