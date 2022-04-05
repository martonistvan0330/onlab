using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class PaymentInfoRepository : IPaymentInfoRepository
    {
        private readonly WebshopDbContext dbContext;

        public PaymentInfoRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> AddPaymentInfo(
            Models.PaymentInfo paymentInfo,
            int billingAddressInfoId,
            int paymentMethodId
        )
        {
            var dbPaymentInfo = new PaymentInfo()
            {
                BillingAddressInfoId = billingAddressInfoId,
                PaymentMethodId = paymentMethodId,
            };
            if (await dbContext.PaymentInfo.ExistsByPaymentInfo(dbPaymentInfo))
            {
                var id = await dbContext.PaymentInfo.GetIdByPaymentInfo(dbPaymentInfo);
                return (true, id);
            }
            else
            {
                dbContext.PaymentInfo.Add(dbPaymentInfo);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbPaymentInfo.Id);
                }
                catch
                {
                    return (false, -1);
                }
            }
        }
    }
}
