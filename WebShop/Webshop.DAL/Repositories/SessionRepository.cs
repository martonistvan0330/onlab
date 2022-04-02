using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly WebshopDbContext dbContext;

        public SessionRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await dbContext.Session.ValidBySessionId(sessionId);
        }

        public async Task<int?> GetUserIdBySessionIdOrNull(string sessionId)
        {
            return await dbContext.Session
                            .FindBySessionId(sessionId)
                            .FilterActive(true)
                            .GetUserIdOrNull();
        }
    }
}
