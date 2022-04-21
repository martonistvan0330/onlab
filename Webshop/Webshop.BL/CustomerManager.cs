using System.Transactions;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class CustomerManager
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IAddressInfoRepository addressInfoRepository;
        private readonly IShippingMethodRepository shippingMethodRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly IShippingInfoRepository shippingInfoRepository;
        private readonly IPaymentInfoRepository paymentInfoRepository;

        private readonly SessionManager sessionManager;

        public CustomerManager(
            ICustomerRepository customerRepository,
            IAddressRepository addressRepository,
            IAddressInfoRepository addressInfoRepository,
            IShippingMethodRepository shippingMethodRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IShippingInfoRepository shippingInfoRepository,
            IPaymentInfoRepository paymentInfoRepository,
            SessionManager sessionManager
        )
        {
            this.customerRepository = customerRepository;
            this.addressRepository = addressRepository;
            this.addressInfoRepository = addressInfoRepository;
            this.shippingMethodRepository = shippingMethodRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.shippingInfoRepository = shippingInfoRepository;
            this.paymentInfoRepository = paymentInfoRepository;
            this.sessionManager = sessionManager;
        }

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await sessionManager.ValidateSessionId(sessionId);
        }

        public async Task<bool> ExistsByName(string customerName, string userId)
        {
            return await customerRepository.ExistsByName(customerName, userId);
        }

        public async Task<bool> ExistsById(string userId, int customerId)
        {
            return await customerRepository.ExistsById(customerId, userId);
        }

        /*public async Task<IReadOnlyCollection<Customer>> ListCustomers(string sessionId)
        {
            var userId = await sessionManager.GetUserIdBySessionIdOrNull(sessionId);
            return await customerRepository.ListCustomers(userId.Value);
        }*/

        public async Task<(bool, int)> TryAddCustomerWithAll(Customer customer, string userId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var (shippingAddressSuccess, shippingAddressId)
                    = await TryAddAddress(customer.ShippingInfo.ShippingAddressInfo.Address);
                var (billingAddressSuccess, billingAddressId)
                    = await TryAddAddress(customer.PaymentInfo.BillingAddressInfo.Address);

                if (shippingAddressSuccess && billingAddressSuccess)
                {
                    var (shippingAddressInfoSuccess, shippingAddressInfoId)
                        = await TryAddAddressInfo(customer.ShippingInfo.ShippingAddressInfo, shippingAddressId);
                    var (billingAddressInfoSuccess, billingAddressInfoId)
                        = await TryAddAddressInfo(customer.PaymentInfo.BillingAddressInfo, billingAddressId);
                    if (shippingAddressInfoSuccess && billingAddressInfoSuccess)
                    {
                        var (shippingInfoSuccess, shippingInfoId)
                            = await TryAddShippingInfo(customer.ShippingInfo, shippingAddressInfoId);
                        var (paymentInfoSuccess, paymentInfoId)
                            = await TryAddPaymentInfo(customer.PaymentInfo, billingAddressInfoId);
                        if (shippingInfoSuccess && paymentInfoSuccess)
                        {
                            var (success, customerId) = await TryAddCustomer(customer, shippingInfoId, paymentInfoId, userId);
                            if (success)
                            {
                                transaction.Complete();
                                return (true, customerId);
                            }
                        }
                    }
                }
                return (false, -1);
            }

        }

        /*public async Task<bool> TryUpdateCustomerWithAll(Customer customer, string? oldName)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var (shippingAddressSuccess, shippingAddressId)
                    = await TryAddAddress(customer.ShippingInfo.ShippingAddressInfo.Address);
                var (billingAddressSuccess, billingAddressId)
                    = await TryAddAddress(customer.PaymentInfo.BillingAddressInfo.Address);

                if (shippingAddressSuccess && billingAddressSuccess)
                {
                    var (shippingAddressInfoSuccess, shippingAddressInfoId)
                        = await TryAddAddressInfo(customer.ShippingInfo.ShippingAddressInfo, shippingAddressId);
                    var (billingAddressInfoSuccess, billingAddressInfoId)
                        = await TryAddAddressInfo(customer.PaymentInfo.BillingAddressInfo, billingAddressId);
                    if (shippingAddressInfoSuccess && billingAddressInfoSuccess)
                    {
                        var (shippingInfoSuccess, shippingInfoId)
                            = await TryAddShippingInfo(customer.ShippingInfo, shippingAddressInfoId);
                        var (paymentInfoSuccess, paymentInfoId)
                            = await TryAddPaymentInfo(customer.PaymentInfo, billingAddressInfoId);
                        if (shippingInfoSuccess && paymentInfoSuccess)
                        {
                            if (await TryUpdateCustomer(customer, shippingInfoId, paymentInfoId, oldName))
                            {
                                transaction.Complete();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

        }*/

        private async Task<(bool, int)> TryAddAddress(Address address)
        {
            return await addressRepository.AddAddress(address);
        }

        private async Task<(bool, int)> TryAddAddressInfo(AddressInfo addressInfo, int addressId)
        {
            return await addressInfoRepository.AddAddressInfo(addressInfo, addressId);
        }

        private async Task<(bool, int)> TryAddShippingInfo(ShippingInfo shippingInfo, int shippingAddressInfoId)
        {
            return await shippingInfoRepository.AddShippingInfo(shippingInfo, shippingAddressInfoId);
        }

        private async Task<(bool, int)> TryAddPaymentInfo(PaymentInfo paymentInfo, int billingAddressInfoId)
        {
            return await paymentInfoRepository.AddPaymentInfo(paymentInfo, billingAddressInfoId);
        }

        private async Task<(bool, int)> TryAddCustomer(Customer customer, int shippingInfoId, int paymentInfoId, string userId)
        {
            if (!(await customerRepository.ExistsByName(customer.Name, userId)))
            {
                return await customerRepository.AddCustomer(customer, shippingInfoId, paymentInfoId, userId);
            }
            else
            {
                return (false, -1);
            }
        }

        /*private async Task<bool> TryUpdateCustomer(Customer customer, int shippingInfoId, int paymentInfoId, string oldName)
        {
            if (await sessionManager.ValidateSessionId(customer.SessionId))
            {
                var userId = await sessionManager.GetUserIdBySessionIdOrNull(customer.SessionId);
                if (await customerRepository.ExistsByName(oldName, userId.Value))
                {
                    return await customerRepository.UpdateCustomer(customer, userId.Value, shippingInfoId, paymentInfoId, oldName);
                }
            }
            return false;
        }*/
    }
}
