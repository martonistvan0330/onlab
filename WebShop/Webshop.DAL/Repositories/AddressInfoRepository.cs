using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class AddressInfoRepository : IAddressInfoRepository
    {
        private readonly WebshopDbContext dbContext;

        public AddressInfoRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> AddAddressInfo(Models.AddressInfo addressInfo, int addressId)
        {
            var dbAddressInfo = new AddressInfo()
            {
                FirstName = addressInfo.FirstName,
                LastName = addressInfo.LastName,
                PhoneNumber = addressInfo.PhoneNumber,
                AddressId = addressId,
            };
            if (await dbContext.AddressInfo.ExistsByAddressInfo(dbAddressInfo, addressId))
            {
                var id = await dbContext.AddressInfo.GetIdByAddressInfo(dbAddressInfo, addressId);
                return (true, id);
            }
            else
            {
                dbContext.AddressInfo.Add(dbAddressInfo);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbAddressInfo.Id);
                }
                catch
                {
                    return (false, -1);
                }
            }
        }
    }
}
