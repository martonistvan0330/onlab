using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class ShippingMethodRepository : IShippingMethodRepository
    {
        private readonly WebshopDbContext dbContext;

        public ShippingMethodRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> GetIdByMethod(string method)
        {
            if (await dbContext.ShippingMethod.ExistsByMethod(method))
            {
                var id = await dbContext.ShippingMethod.GetIdByMethod(method);
                return (true, id);
            }
            else
            {
                return (false, -1);
            }
        }
    }
}
