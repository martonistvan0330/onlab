using System.Transactions;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class CustomerManager
    {
        public readonly ICustomerRepository customerRepository;
        public readonly ISessionRepository sessionRepository;
        public readonly IAddressRepository addressRepository;
        public readonly IAddressInfoRepository addressInfoRepository;
        public readonly IShippingMethodRepository shippingMethodRepository;
        public readonly IPaymentMethodRepository paymentMethodRepository;
        public readonly IShippingInfoRepository shippingInfoRepository;
        public readonly IPaymentInfoRepository paymentInfoRepository;

        public CustomerManager(
            ICustomerRepository customerRepository,
            ISessionRepository sessionRepository,
            IAddressRepository addressRepository,
            IAddressInfoRepository addressInfoRepository,
            IShippingMethodRepository shippingMethodRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IShippingInfoRepository shippingInfoRepository,
            IPaymentInfoRepository paymentInfoRepository
        )
        {
            this.customerRepository = customerRepository;
            this.sessionRepository = sessionRepository;
            this.addressRepository = addressRepository;
            this.addressInfoRepository = addressInfoRepository;
            this.shippingMethodRepository = shippingMethodRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.shippingInfoRepository = shippingInfoRepository;
            this.paymentInfoRepository = paymentInfoRepository;
        }

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await sessionRepository.ValidateSessionId(sessionId);
        }

        public async Task<bool> ExistsByName(string customerName, string sessionId)
        {
            var userId = await sessionRepository.GetUserIdBySessionIdOrNull(sessionId);
            return await customerRepository.ExistsByName(customerName, userId.Value);
        }

        public async Task<IReadOnlyCollection<Customer>> ListCustomers(string sessionId)
        {
            var userId = await sessionRepository.GetUserIdBySessionIdOrNull(sessionId);
            return await customerRepository.ListCustomers(userId.Value);
        }

        public async Task<bool> TryAddCustomerWithAll(Customer customer)
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
                            if (await TryAddCustomer(customer, shippingInfoId, paymentInfoId))
                            {
                                transaction.Complete();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

        }

        public async Task<bool> TryUpdateCustomerWithAll(Customer customer, string? oldName)
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

        }

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
            var (shippingMethodSuccess, shippingMethodId)
                = await shippingMethodRepository.GetIdByMethod(shippingInfo.ShippingMethod.Method);
            if (shippingMethodSuccess)
            {
                return await shippingInfoRepository.AddShippingInfo(shippingInfo, shippingAddressInfoId, shippingMethodId);
            }
            else
            {
                return (false, -1);
            }
        }

        private async Task<(bool, int)> TryAddPaymentInfo(PaymentInfo paymentInfo, int billingAddressInfoId)
        {
            var (paymentMethodSuccess, paymentMethodId)
                = await paymentMethodRepository.GetIdByMethod(paymentInfo.PaymentMethod.Method);
            if (paymentMethodSuccess)
            {
                return await paymentInfoRepository.AddPaymentInfo(paymentInfo, billingAddressInfoId, paymentMethodId);
            }
            else
            {
                return (false, -1);
            }
        }

        private async Task<bool> TryAddCustomer(Customer customer, int shippingInfoId, int paymentInfoId)
        {
            if (await sessionRepository.ValidateSessionId(customer.SessionId))
            {
                var userId = await sessionRepository.GetUserIdBySessionIdOrNull(customer.SessionId);
                if (!(await customerRepository.ExistsByName(customer.Name, userId.Value)))
                {
                    return await customerRepository.AddCustomer(customer, userId.Value, shippingInfoId, paymentInfoId);
                }
            }
            return false;
        }

        private async Task<bool> TryUpdateCustomer(Customer customer, int shippingInfoId, int paymentInfoId, string oldName)
        {
            if (await sessionRepository.ValidateSessionId(customer.SessionId))
            {
                var userId = await sessionRepository.GetUserIdBySessionIdOrNull(customer.SessionId);
                if (await customerRepository.ExistsByName(oldName, userId.Value))
                {
                    return await customerRepository.UpdateCustomer(customer, userId.Value, shippingInfoId, paymentInfoId, oldName);
                }
            }
            return false;
        }
    }
}
