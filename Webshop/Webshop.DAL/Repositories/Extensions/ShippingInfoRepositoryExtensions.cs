using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class ShippingInfoRepositoryExtensions
    {
        public static IQueryable<ShippingInfo> FilterByShippingAddressInfoId(this IQueryable<ShippingInfo> shippingInfos, int shippingAddressInfoId)
        {
            return shippingInfos.Where(si => si.ShippingAddressInfoId == shippingAddressInfoId);
        }

        public static IQueryable<ShippingInfo> FilterByShippingMethodId(this IQueryable<ShippingInfo> shippingInfos, int shippingMethodId)
        {
            return shippingInfos.Where(si => si.ShippingMethodId == shippingMethodId);
        }

        public static IQueryable<ShippingInfo> FindByShippingInfo(this IQueryable<ShippingInfo> shippingInfos, ShippingInfo shippingInfo)
        {
            return shippingInfos
                    .FilterByShippingAddressInfoId(shippingInfo.ShippingAddressInfoId)
                    .FilterByShippingMethodId(shippingInfo.ShippingMethodId);
        }

        public static async Task<bool> ExistsByShippingInfo(this IQueryable<ShippingInfo> shippingInfos, ShippingInfo shippingInfo)
        {
            var dbShippingInfo = await shippingInfos
                            .FindByShippingInfo(shippingInfo)
                            .GetShippingInfoOrNull();
            if (dbShippingInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByShippingInfo(this IQueryable<ShippingInfo> shippingInfos, ShippingInfo shippingInfo)
        {
            return await shippingInfos
                            .FindByShippingInfo(shippingInfo)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<ShippingInfo> shippingInfos)
        {
            return await shippingInfos
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<ShippingInfo?> GetShippingInfoOrNull(this IQueryable<ShippingInfo> shippingInfos)
            => await shippingInfos.SingleOrDefaultAsync();
    }
}
