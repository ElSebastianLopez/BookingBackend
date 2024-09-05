using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly ContextModel _contextModel;
        public CustomerRepository(ContextModel contextModel)
        {

            _contextModel = contextModel;

        }

        public async Task<bool> CreateCustomer(CustomerModel customer)
        {
            await _contextModel.Customer.AddAsync(customer);
            return await Save();
        }

        public async Task<bool> DeleteCustomer(CustomerModel customer)
        {
            _contextModel.Customer.Remove(customer);
            return await Save();
        }

        public async Task<bool> EditCustomer(CustomerModel customer)
        {
            _contextModel.Customer.Update(customer);
            return await Save();
        }

        public async Task<List<CustomerModel>> GetAllUCustomer(Expression<Func<CustomerModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _contextModel.Customer.ToListAsync();
            }
            return await _contextModel.Customer.Where(predicate).ToListAsync();
        }

        public async Task<CustomerModel> GetByIdCustomer(int id)
        {
            return await _contextModel.Customer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _contextModel.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
