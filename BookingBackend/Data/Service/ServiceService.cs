using ApiBook.Models.DTOs;
using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model;
using BookingBackend.Model.DTO;

namespace BookingBackend.Data.Service
{
    public class ServiceService: IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Response> GetAllService()
        {
            Response res = new Response();
            try
            {
                List<ServiceModel> AllService = await _serviceRepository.GetAllService();
                List<ServiceDTO> AllServiceDTO = new List<ServiceDTO>();
                foreach (ServiceModel service in AllService)
                {
                    ServiceDTO serviceDTO = new ServiceDTO()
                    {
                        Name = service.Name,
                        Description = service.Description,
                        Id = service.Id,
                        Price = service.Price,
                    };
                    AllServiceDTO.Add(serviceDTO);

                }
                res.Succes = true;
                res.Data = AllServiceDTO;
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

        public async Task<Response> GetByIdService(int id)
        {
            Response res = new Response();
            try
            {
                ServiceModel service = await _serviceRepository.GetByIdService(id);
                ServiceDTO serviceDTO = new ServiceDTO()
                {
                    Name = service.Name,
                    Description = service.Description,
                    Id = service.Id,
                    Price = service.Price,
                };

                res.Succes = true;
                res.Data = serviceDTO;
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
