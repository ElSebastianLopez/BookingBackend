using ApiBook.Models.DTOs;

namespace BookingBackend.Data.Service.IService
{
    public interface IServiceService
    {
        Task<Response> GetAllService();
        Task<Response> GetByIdService(int id);
    }
}
