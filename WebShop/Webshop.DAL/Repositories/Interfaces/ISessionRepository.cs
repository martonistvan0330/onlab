namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<bool> ValidateSessionId(string sessionId);
        Task<int?> GetUserIdBySessionIdOrNull(string sessionId);
    }
}
