using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IPaymentInfoRepository
    {
        Task<(bool, int)> AddPaymentInfo(PaymentInfo paymentInfo, int billingAddressInfoId);
    }
}
