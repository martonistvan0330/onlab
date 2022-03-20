using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class AddressRepositoryExtensions
    {
        public static IQueryable<Address> FilterByCountry(this IQueryable<Address> addresses, string country)
        {
            return addresses.Where(a => a.Country.Equals(country));
        }
        public static IQueryable<Address> FilterByRegion(this IQueryable<Address> addresses, string region)
        {
            return addresses.Where(a => a.Region.Equals(region));
        }
        public static IQueryable<Address> FilterByCity(this IQueryable<Address> addresses, string city)
        {
            return addresses.Where(a => a.City.Equals(city));
        }
        public static IQueryable<Address> FilterByZipCode(this IQueryable<Address> addresses, string zipCode)
        {
            return addresses.Where(a => a.ZipCode.Equals(zipCode));
        }
        public static IQueryable<Address> FilterByStreet(this IQueryable<Address> addresses, string street)
        {
            return addresses.Where(a => a.Street.Equals(street));
        }
        public static IQueryable<Address> FindByAddress(this IQueryable<Address> addresses, Address address)
        {
            return addresses
                    .FilterByCountry(address.Country)
                    .FilterByRegion(address.Region)
                    .FilterByZipCode(address.ZipCode)
                    .FilterByCity(address.City)
                    .FilterByStreet(address.Street);
        }

        public static async Task<bool> ExistsByAddress(this IQueryable<Address> addresses, Address address)
        {
            var dbAddress = await addresses
                            .FindByAddress(address)
                            .GetAddressOrNull();
            if (dbAddress == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int> GetIdByAddress(this IQueryable<Address> addresses, Address address)
        {
            return await addresses
                            .FindByAddress(address)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<Address> addresses)
        {
            return await addresses
                        .Select(a => a.Id)
                        .SingleAsync();
        }

        public static async Task<Address?> GetAddressOrNull(this IQueryable<Address> addresses) 
            => await addresses.SingleOrDefaultAsync();
    }
}
