using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class SessionRepositoryExtensions
    {
        public static IQueryable<Session> FindBySessionId(this IQueryable<Session> sessions, string sessionId)
        {
            return sessions.Where(s => s.SessionId.Equals(sessionId));
        }

        public static IQueryable<Session> FilterActive(this IQueryable<Session> sessions, bool active)
        {
            return sessions.Where(s => s.IsActive == active);
        }

        public static async Task<bool> ValidBySessionId(this IQueryable<Session> sessions, string sessionId)
        {
            var dbSession = await sessions
                                    .FindBySessionId(sessionId)
                                    .FilterActive(true)
                                    .GetSessionOrNull();
            if (dbSession == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<int?> GetUserIdOrNull(this IQueryable<Session> sessions)
        {
            return await sessions
                            .Select(s => s.UserId)
                            .SingleOrDefaultAsync();
        }

        public static async Task<Session?> GetSessionOrNull(this IQueryable<Session> sessions)
            => await sessions.SingleOrDefaultAsync();
    }
}
