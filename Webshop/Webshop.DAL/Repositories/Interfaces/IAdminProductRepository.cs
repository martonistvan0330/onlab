using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IAdminProductRepository
    {
        Task<int> AddProduct(NewProduct newProduct);
        Task<int> UpdateProduct(int productId, NewProduct newProduct);
        Task<ProductDetails?> GetProductDetailsOrNull(int productId);
    }
}
