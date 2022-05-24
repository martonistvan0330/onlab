using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IAdminProductStockRepository
    {
        Task<bool> AddNewStock(int productId, int sizeId, int amount);
        Task<bool> RefreshStock(int productId, int sizeId, int amount);
        Task<int> GetStockByProductSizeOrNull(int productId, int sizeId);
    }
}
