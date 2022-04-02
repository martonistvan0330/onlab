using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly WebshopDbContext dbContext;

        public PaymentMethodRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> GetIdByMethod(string method)
        {
            if (await dbContext.PaymentMethod.ExistsByMethod(method))
            {
                var id = await dbContext.PaymentMethod.GetIdByMethod(method);
                return (true, id);
            }
            else
            {
                return (false, -1);
            }
        }
    }
}
