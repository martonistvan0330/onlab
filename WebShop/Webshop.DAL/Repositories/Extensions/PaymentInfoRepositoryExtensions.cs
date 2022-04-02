using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class PaymentInfoRepositoryExtensions
    {
        public static IQueryable<PaymentInfo> FilterByBillingAddressInfoId(this IQueryable<PaymentInfo> paymentInfos, int billingAddressInfoId)
        {
            return paymentInfos.Where(si => si.BillingAddressInfoId == billingAddressInfoId);
        }

        public static IQueryable<PaymentInfo> FilterByPaymentMethodId(this IQueryable<PaymentInfo> paymentInfos, int paymentMethodId)
        {
            return paymentInfos.Where(si => si.PaymentMethodId == paymentMethodId);
        }

        public static IQueryable<PaymentInfo> FindByPaymentInfo(this IQueryable<PaymentInfo> paymentInfos, PaymentInfo paymentInfo)
        {
            return paymentInfos
                    .FilterByBillingAddressInfoId(paymentInfo.BillingAddressInfoId)
                    .FilterByPaymentMethodId(paymentInfo.PaymentMethodId);
        }

        public static async Task<bool> ExistsByPaymentInfo(this IQueryable<PaymentInfo> paymentInfos, PaymentInfo paymentInfo)
        {
            var dbPaymentInfo = await paymentInfos
                            .FindByPaymentInfo(paymentInfo)
                            .GetPaymentInfoOrNull();
            if (dbPaymentInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByPaymentInfo(this IQueryable<PaymentInfo> paymentInfos, PaymentInfo paymentInfo)
        {
            return await paymentInfos
                            .FindByPaymentInfo(paymentInfo)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<PaymentInfo> paymentInfos)
        {
            return await paymentInfos
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<PaymentInfo?> GetPaymentInfoOrNull(this IQueryable<PaymentInfo> paymentInfos)
            => await paymentInfos.SingleOrDefaultAsync();
    }
}
