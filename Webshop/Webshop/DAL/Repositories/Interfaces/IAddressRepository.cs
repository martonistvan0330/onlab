using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<(bool, int)> AddAddress(Address address);
    }
}
