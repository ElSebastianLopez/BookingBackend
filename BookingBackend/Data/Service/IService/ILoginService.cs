namespace BookingBackend.Data.Service.IService
{
    public interface ILoginService
    {
        Task<object> Login(string email, string password);
    }
}
