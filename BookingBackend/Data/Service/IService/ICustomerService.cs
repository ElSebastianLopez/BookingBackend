using ApiBook.Models.DTOs;

namespace BookingBackend.Data.Service.IService
{
    public interface ICustomerService
    {
        Task<Response> GetByIdCustomer(int id);
        Task<Response> GetAllCustomer();
    }
}
