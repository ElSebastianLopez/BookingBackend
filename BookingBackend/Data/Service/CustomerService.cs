using BookingBackend.Data.Repository;
using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model.DTO;
using BookingBackend.Model;
using ApiBook.Models.DTOs;

namespace BookingBackend.Data.Service
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Response> GetAllCustomer()
        {
            Response res = new Response();
            try
            {
                List<CustomerModel> AllCustomer = await _customerRepository.GetAllUCustomer();
                List<CustomerDTO> AllCustomerDTO = new List<CustomerDTO>();
                foreach (CustomerModel custo in AllCustomer)
                {
                    CustomerDTO serviceDTO = new CustomerDTO()
                    {
                        Id = custo.Id,
                        CellPhone = custo.CellPhone,
                        Email = custo.Email,
                        FullName = custo.FullName,  
                        Nit = custo.Nit,
                        Password = custo.Password   

                        
                        
                    };
                    AllCustomerDTO.Add(serviceDTO);

                }
                res.Succes = true;
                res.Data = AllCustomerDTO;
                res.Message = "Ok";
                return res;

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }

        public async Task<Response> GetByIdCustomer(int id)
        {
            Response res = new Response();
            try
            {
                CustomerModel custo = await _customerRepository.GetByIdCustomer(id);
                List<CustomerDTO> CustomerDTO = new List<CustomerDTO>();
                if (custo != null)
                {
                    CustomerDTO serviceDTO = new CustomerDTO()
                    {
                        Id = custo.Id,
                        CellPhone = custo.CellPhone,
                        Email = custo.Email,
                        FullName = custo.FullName,
                        Nit = custo.Nit,
                        Password = custo.Password
                    };
                }
                
                    
                res.Succes = true;
                res.Data = CustomerDTO;
                res.Message = "Ok";
                return res;

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }
    }
}
