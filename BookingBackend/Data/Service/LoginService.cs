using ApiBook.Models.DTOs;
using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

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

                var tokenService = new GenerarTokenClaim(_config);
                var token = tokenService.GenerateToken(customer.FirstOrDefault());
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

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
