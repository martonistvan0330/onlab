using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private WebshopDbContext dbContext;

        public SizeRepository(WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByName(string sizeName)
        {
            var size = await dbContext.Size.Where(s => s.Name.Equals(sizeName)).SingleOrDefaultAsync();
            return size != null;
        }

        public async Task<int> GetIdByName(string sizeName)
        {
            return await dbContext.Size.Where(s => s.Name.Equals(sizeName)).Select(s => s.Id).SingleAsync();
        }
    }
}
