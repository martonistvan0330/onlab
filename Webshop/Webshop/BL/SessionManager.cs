using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class SessionManager
    {
        private readonly ISessionRepository sessionRepository;

        public SessionManager(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await sessionRepository.ValidateSessionId(sessionId);
        }

        public async Task<int?> GetUserIdBySessionIdOrNull(string sessionId)
        {
            return await sessionRepository.GetUserIdBySessionIdOrNull(sessionId);
        }
    }
}
