using BookingBackend.Model;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository.IRepository
{
    public interface IServiceRepository
    {
        Task<bool> CreateService(ServiceModel service);
        Task<bool> DeleteService(ServiceModel service);
        Task<bool> EditService(ServiceModel service);
        Task<List<ServiceModel>> GetAllService(Expression<Func<ServiceModel, bool>> predicate = null);
        Task<ServiceModel> GetByIdService(int id);
        Task<bool> Save();
    }
}
