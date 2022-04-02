using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class PaymentMethodRepositoryExtensions
    {
        public static IQueryable<PaymentMethod> FindByMethod(this IQueryable<PaymentMethod> paymentMethods, string method)
        {
            return paymentMethods
                    .Where(sm => sm.Method.Equals(method));
        }

        public static async Task<bool> ExistsByMethod(this IQueryable<PaymentMethod> paymentMethods, string method)
        {
            var dbPaymentMethod = await paymentMethods
                            .FindByMethod(method)
                            .GetPaymentMethodOrNull();
            if (dbPaymentMethod == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByMethod(this IQueryable<PaymentMethod> paymentMethods, string method)
        {
            return await paymentMethods
                            .FindByMethod(method)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<PaymentMethod> paymentMethods)
        {
            return await paymentMethods
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<PaymentMethod?> GetPaymentMethodOrNull(this IQueryable<PaymentMethod> paymentMethods)
            => await paymentMethods.SingleOrDefaultAsync();
    }
}
