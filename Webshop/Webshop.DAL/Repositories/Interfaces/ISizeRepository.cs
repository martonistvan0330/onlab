namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ISizeRepository
    {
        Task<bool> ExistsByName(string sizeName);
        Task<int> GetIdByName(string sizeName);
    }
}
