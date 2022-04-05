namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<(bool, int)> GetIdByMethod(string method);
    }
}
