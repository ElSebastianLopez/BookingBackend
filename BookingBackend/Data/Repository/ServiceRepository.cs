using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository
{
    public class ServiceRepository: IServiceRepository
    {
        private readonly ContextModel _contextModel;
        public ServiceRepository(ContextModel contextModel)
        {

            _contextModel = contextModel;

        }

        public async Task<bool> CreateService(ServiceModel service)
        {
            await _contextModel.Service.AddAsync(service);
            return await Save();
        }

        public async Task<bool> DeleteService(ServiceModel service)
        {
            _contextModel.Service.Remove(service);
            return await Save();
        }

        public async Task<bool> EditService(ServiceModel service)
        {
            _contextModel.Service.Update(service);
            return await Save();
        }

        public async Task<List<ServiceModel>> GetAllService(Expression<Func<ServiceModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _contextModel.Service.ToListAsync();
            }
            return await _contextModel.Service.Where(predicate).ToListAsync();
        }

        public async Task<ServiceModel> GetByIdService(int id)
        {
            return await _contextModel.Service.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _contextModel.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
