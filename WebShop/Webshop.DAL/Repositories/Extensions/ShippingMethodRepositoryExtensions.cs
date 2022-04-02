using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class ShippingMethodRepositoryExtensions
    {
        public static IQueryable<ShippingMethod> FindByMethod(this IQueryable<ShippingMethod> shippingMethods, string method)
        {
            return shippingMethods
                    .Where(sm => sm.Method.Equals(method));
        }

        public static async Task<bool> ExistsByMethod(this IQueryable<ShippingMethod> shippingMethods, string method)
        {
            var dbShippingMethod = await shippingMethods
                            .FindByMethod(method)
                            .GetShippingMethodOrNull();
            if (dbShippingMethod == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByMethod(this IQueryable<ShippingMethod> shippingMethods, string method)
        {
            return await shippingMethods
                            .FindByMethod(method)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<ShippingMethod> shippingMethods)
        {
            return await shippingMethods
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<ShippingMethod?> GetShippingMethodOrNull(this IQueryable<ShippingMethod> shippingMethods)
            => await shippingMethods.SingleOrDefaultAsync();
    }
}
