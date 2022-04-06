using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class ShippingInfoRepository : IShippingInfoRepository
    {
        private readonly WebshopDbContext dbContext;

        public ShippingInfoRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> AddShippingInfo(
            Models.ShippingInfo shippingInfo,
            int shippingAddressInfoId,
            int shippingMethodId
        )
        {
            var dbShippingInfo = new ShippingInfo()
            {
                ShippingAddressInfoId = shippingAddressInfoId,
                ShippingMethodId = shippingMethodId,
            };
            if (await dbContext.ShippingInfo.ExistsByShippingInfo(dbShippingInfo))
            {
                var id = await dbContext.ShippingInfo.GetIdByShippingInfo(dbShippingInfo);
                return (true, id);
            }
            else
            {
                dbContext.ShippingInfo.Add(dbShippingInfo);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbShippingInfo.Id);
                }
                catch
                {
                    return (false, -1);
                }
            }
        }
    }
}
