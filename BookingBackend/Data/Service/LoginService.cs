using BookingBackend.Data.Repository.IRepository;

namespace BookingBackend.Data.Service
{
    public class LoginService
    {
        private readonly ICustomerRepository _customerRepository;
        public LoginService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

       
    }
}
