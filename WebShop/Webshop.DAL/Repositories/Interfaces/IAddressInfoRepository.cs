using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IAddressInfoRepository
    {
        Task<(bool, int)> AddAddressInfo(AddressInfo addressInfo, int addressId);
    }
}
