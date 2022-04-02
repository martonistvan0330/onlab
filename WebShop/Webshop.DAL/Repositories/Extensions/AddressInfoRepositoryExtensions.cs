using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class AddressInfoRepositoryExtensions
    {
        public static IQueryable<AddressInfo> FilterByAddressId(this IQueryable<AddressInfo> addressInfos, int addressId)
        {
            return addressInfos.Where(ai => ai.AddressId == addressId);
        }
        public static IQueryable<AddressInfo> FilterByFirstName(this IQueryable<AddressInfo> addressInfo, string firstName)
        {
            return addressInfo.Where(ai => ai.FirstName.Equals(firstName));
        }
        public static IQueryable<AddressInfo> FilterByLastName(this IQueryable<AddressInfo> addressInfo, string lastName)
        {
            return addressInfo.Where(ai => ai.LastName.Equals(lastName));
        }
        public static IQueryable<AddressInfo> FilterByPhoneNumber(this IQueryable<AddressInfo> addressInfo, string phoneNumber)
        {
            return addressInfo.Where(ai => ai.PhoneNumber.Equals(phoneNumber));
        }
        
        public static IQueryable<AddressInfo> FindByAddressInfo(this IQueryable<AddressInfo> addressInfos, AddressInfo addressInfo, int addressId)
        {
            return addressInfos
                    .FilterByAddressId(addressId)
                    .FilterByFirstName(addressInfo.FirstName)
                    .FilterByLastName(addressInfo.LastName)
                    .FilterByPhoneNumber(addressInfo.PhoneNumber);
        }

        public static async Task<bool> ExistsByAddressInfo(this IQueryable<AddressInfo> addressInfos, AddressInfo addressInfo, int addressId)
        {
            var dbAddressInfo = await addressInfos
                            .FindByAddressInfo(addressInfo, addressId)
                            .GetAddressInfoOrNull();
            if (dbAddressInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByAddressInfo(this IQueryable<AddressInfo> addressInfos, AddressInfo addressInfo, int addressId)
        {
            return await addressInfos
                            .FindByAddressInfo(addressInfo, addressId)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<AddressInfo> addressInfos)
        {
            return await addressInfos
                        .Select(ai => ai.Id)
                        .SingleAsync();
        }

        public static async Task<AddressInfo?> GetAddressInfoOrNull(this IQueryable<AddressInfo> addressInfos)
            => await addressInfos.SingleOrDefaultAsync();
    }
}
