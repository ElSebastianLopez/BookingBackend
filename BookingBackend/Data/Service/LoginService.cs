using ApiBook.Models.DTOs;
using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace BookingBackend.Data.Service
{
    public class LoginService: ILoginService
    {
        private readonly ICustomerRepository _customerRepository;
        private IConfiguration _config;
        public LoginService(ICustomerRepository customerRepository, IConfiguration config)
        {
            _customerRepository = customerRepository;
            _config = config;
        }

        public async Task<object> Login(string email, string password)
        {
            Response res = new Response();
            try
            {
                List<CustomerModel> customer = await _customerRepository.GetAllUCustomer(x => x.Email == email && x.Password == password);
                if (customer.FirstOrDefault() == null)
                {
                    res.Message = "Credenciales incorrectas o no existentes";
                    res.Succes = false;
                    return new { res = res };
                }
                GenerarTokenClaim generarTokenClaim = new GenerarTokenClaim(_config);
                string token = generarTokenClaim.GenerateToken(customer.FirstOrDefault());
                res.Message = "Credenciales correctas";
                res.Succes = true;
                res.Data = customer;

                return new { token,res};
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }



    }
}
