using BookingBackend.Model;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(CustomerModel customer);
        Task<bool> DeleteCustomer(CustomerModel customer);
        Task<bool> EditCustomer(CustomerModel customer);
        Task<List<CustomerModel>> GetAllUCustomer(Expression<Func<CustomerModel, bool>> predicate = null);
        Task<CustomerModel> GetByIdCustomer(int id);
        Task<bool> Save();
    }
}
