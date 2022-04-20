using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IShippingInfoRepository
    {
        Task<(bool, int)> AddShippingInfo(ShippingInfo shippingInfo, int shippingAddressInfoId);
    }
}
