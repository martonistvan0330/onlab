using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly WebshopDbContext dbContext;

        public AddressRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> AddAddress(Models.Address address)
        {
            var dbAddress = new Address()
            {
                Country = address.Country,
                Region = address.Region,
                City = address.City,
                ZipCode = address.ZipCode,
                Street = address.Street,
            };
            if (await dbContext.Address.ExistsByAddress(dbAddress))
            {
                var id = await dbContext.Address.GetIdByAddress(dbAddress);
                return (true, id);
            }
            else
            {
                dbContext.Address.Add(dbAddress);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbAddress.Id);
                }
                catch
                {
                    return (false, -1);
                }
            }
        }
    }
}
