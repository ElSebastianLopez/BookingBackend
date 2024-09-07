using ApiBook.Models.DTOs;
using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model;
using BookingBackend.Model.DTO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static BookingBackend.Model.Enums;

namespace BookingBackend.Data.Service
{
    public class ReservationService: IReservationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationDetailRepository _reservationDetailRepository;
        public ReservationService(ICustomerRepository customerRepository, IServiceRepository serviceRepository, IReservationRepository reservationRepository, IReservationDetailRepository reservationDetailRepository)
        {
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
            _reservationRepository = reservationRepository;
            _reservationDetailRepository = reservationDetailRepository;
        }

        public async Task<Response> GetAllReservationByIdCustomer(int id)
        {
            Response res = new Response();
            try
            {
                List<ReservationModel> AllReservationByCustomer = await _reservationRepository.GetAllReservation(x => x.IdCustomer == id);
                List<ReservationDTO> AllreservationByCustomerDTO = new List<ReservationDTO>();
                foreach (ReservationModel item in AllReservationByCustomer)
                {
                    ReservationDTO reservationDTO = new ReservationDTO();
                    reservationDTO.IdCustomer = item.IdCustomer;
                    reservationDTO.ReservationDate = item.ReservationDate;
                    reservationDTO.ReservationStartDate = item.ReservationStartDate;
                    reservationDTO.ReservationEndDate = item.ReservationEndDate;
                    List<ReservationDetailModel> AllreservationDetailsByIdreservation = await _reservationDetailRepository.GetAllReservationDetail(x => x.IdReservation == item.Id);
                    foreach (var item2 in AllreservationDetailsByIdreservation)
                    {
                        ReservationDetailDTO reservationDetailDTO = new ReservationDetailDTO()
                        {
                            Id = item2.Id,
                            IdReservation = item2.IdReservation,
                            IdService = item2.IdService,
                            ServiceCost = item2.ServiceCost,
                            service=item2.Service.Name
                            
                        };
                        reservationDTO.Details.Add(reservationDetailDTO);

                    }
                    reservationDTO.Id = item.Id;
                    reservationDTO.Name = item.Name;
                    if (item.Status == Status.Creada)
                    {
                        reservationDTO.status = "Creada";
                    }
                    else if (item.Status == Status.Pagada)
                    {
                        reservationDTO.status = "Pagada";
                    }
                    else if (item.Status == Status.Cancelada)
                    {
                        reservationDTO.status = "Cancelada";
                    }
                    else
                    {
                        reservationDTO.status = "Eror";
                    }
                    reservationDTO.TotalReserveCost = item.TotalReserveCost;

                    AllreservationByCustomerDTO.Add(reservationDTO);
                }
                res.Succes = true;
                res.Message = "Ok";
                res.Data = AllreservationByCustomerDTO;
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

        public async Task<Response> GetByIdReservation(int id)
        {
            Response res = new Response();
            try
            {
                ReservationModel ReservationByCustomer = await _reservationRepository.GetByIdReservation(id);


                ReservationDTO reservationDTO = new ReservationDTO();
                reservationDTO.IdCustomer = ReservationByCustomer.IdCustomer;
                reservationDTO.ReservationDate = ReservationByCustomer.ReservationDate;
                reservationDTO.ReservationStartDate = ReservationByCustomer.ReservationStartDate;
                reservationDTO.ReservationEndDate = ReservationByCustomer.ReservationEndDate;
                List<ReservationDetailModel> AllreservationDetailsByIdreservation = await _reservationDetailRepository.GetAllReservationDetail(x => x.IdReservation == ReservationByCustomer.Id);
                foreach (var item2 in AllreservationDetailsByIdreservation)
                {
                    ReservationDetailDTO reservationDetailDTO = new ReservationDetailDTO()
                    {
                        Id = item2.Id,
                        IdReservation = item2.IdReservation,
                        IdService = item2.IdService,
                        ServiceCost = item2.ServiceCost,
                    };
                    reservationDTO.Details.Add(reservationDetailDTO);

                }
                reservationDTO.Id = ReservationByCustomer.Id;

                res.Succes = true;
                res.Message = "Ok";
                res.Data = reservationDTO;
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

        public async Task<Response> AddOrEditReservation(ReservationDTO reservationDTO)
        {
            Response res = new Response();
            try
            {
                CustomerModel customer = await _customerRepository.GetByIdCustomer((int)reservationDTO.IdCustomer);
                if (customer == null)
                {
                    throw new InvalidOperationException("No existe un customer con el id" + reservationDTO.IdCustomer);
                }
                if (reservationDTO.ReservationStartDate <= DateTime.Now)
                {
                    throw new InvalidOperationException("La fecha de reservacion debe ser mayor a la fecha de hoy" + reservationDTO.ReservationStartDate);
                }
                if (reservationDTO.ReservationEndDate < reservationDTO.ReservationStartDate)
                {
                    throw new InvalidOperationException("La fecha final de reservacion debe ser mayor a la fecha de inicio" + reservationDTO.ReservationStartDate);
                }
                foreach (var item in reservationDTO.Details)
                {
                    ServiceModel service = await _serviceRepository.GetByIdService(item.IdService);
                    if (service == null)
                    {
                        throw new InvalidOperationException("No existe un servicio con el id " + item.IdService);
                    }
                }
                if (reservationDTO.Id == 0)
                {
                    ReservationModel reservationModel = new ReservationModel()
                    {
                        IdCustomer = (int)reservationDTO.IdCustomer,
                        ReservationStartDate = reservationDTO.ReservationStartDate,
                        ReservationEndDate = reservationDTO.ReservationEndDate,
                        ReservationDate = DateTime.Now,
                        Name = DateTime.Now.ToString("'Reserva del día' dddd dd 'de' MMMM 'del' yyyy", new CultureInfo("es-ES")),
                        TotalReserveCost = reservationDTO.Details.Sum(x => x.ServiceCost).ToString(),
                        Status = Status.Creada
                    };
                    bool save = await _reservationRepository.CreateReservation(reservationModel);
                    if (save)
                    {
                        foreach (var item in reservationDTO.Details)
                        {
                            ReservationDetailModel detailModel = new ReservationDetailModel()
                            {
                                IdReservation = reservationModel.Id,
                                IdService = item.IdService,
                                ServiceCost = item.ServiceCost,
                            };
                            await _reservationDetailRepository.CreateReservationDetail(detailModel);

                        }
                        res.Succes = true;
                        res.Message = "reserva creada satisfactoriamnete";
                        return res;


                    }
                    throw new InvalidOperationException("Error al guardar informacion, Contactese con los administradores de el aplicativo");

                }
                else
                {
                    ReservationModel EditReserva = await _reservationRepository.GetByIdReservation((int)reservationDTO.Id);
                    List<ReservationDetailModel> reservationDet = await _reservationDetailRepository.GetAllReservationDetail(x => x.IdReservation == EditReserva.Id);
                    if (EditReserva.Status == Status.Creada)
                    {
                        EditReserva.ReservationStartDate = reservationDTO.ReservationStartDate;
                        EditReserva.ReservationEndDate = reservationDTO.ReservationEndDate;
                        EditReserva.TotalReserveCost = reservationDTO.Details.Sum(x => x.ServiceCost).ToString();
                        bool edit = await _reservationRepository.EditReservation(EditReserva);
                        if ((edit))
                        {
                            var existingDetails = reservationDet.Where(x => x.IdReservation == EditReserva.Id).ToList();
                            var newServiceIds = reservationDTO.Details.Select(x => x.IdService).ToHashSet();

                            foreach (var item in reservationDTO.Details)
                            {
                                item.IdReservation = EditReserva.Id;
                                if (!existingDetails.Any(x => x.IdService == item.IdService))
                                {
                                    ReservationDetailModel detailModel = new ReservationDetailModel()
                                    {
                                        IdReservation = EditReserva.Id,
                                        IdService = item.IdService,
                                        ServiceCost = item.ServiceCost,
                                    };
                                    await _reservationDetailRepository.CreateReservationDetail(detailModel);
                                }
                            }

                            var detailsToRemove = existingDetails.Where(x => !newServiceIds.Contains(x.IdService)).ToList();

                            foreach (var detail in detailsToRemove)
                            {
                                await _reservationDetailRepository.DeleteReservationDetail(detail);
                            }

                        }
                        res.Succes = true;
                        res.Message = "Reserva editada satisfactoriamnete";
                        return res;
                    }

                    throw new InvalidOperationException("Error al guardar informacion, No se puede editar una reserva que cuyo estado no sea creado");

                }

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

        public async Task<Response> DeleteReservation(int id)
        {
            Response res = new Response();
            try
            {
                ReservationModel EditReserva = await _reservationRepository.GetByIdReservation(id);
                List<ReservationDetailModel> reservationDet = await _reservationDetailRepository.GetAllReservationDetail(x => x.IdReservation == EditReserva.Id);
                if (EditReserva.Status == Status.Creada)
                {
                    foreach (var iten in reservationDet)
                    {
                        await _reservationDetailRepository.DeleteReservationDetail(iten);
                    }
                    await _reservationRepository.DeleteReservation(EditReserva);

                    res.Succes = true;
                    res.Message = "Su reserva fue eliminada correctamente";
                    return res;

                }
                throw new InvalidOperationException("No se puede eliminar una orden cuyo estado no sea creado ");

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

        public async Task<Response> DeleteReservationDet(int idReservation,int idReservationDet)
        {
            Response res = new Response();
            try
            {
                ReservationModel Reserva = await _reservationRepository.GetByIdReservation(idReservation);
                if(Reserva.Status==Status.Creada || Reserva.Status == Status.Cancelada)
                {
                    ReservationDetailModel reservationDet = await _reservationDetailRepository.GetByIdReservationDetail(idReservationDet);
                    if (reservationDet != null)
                    {

                        await _reservationDetailRepository.DeleteReservationDetail(reservationDet);
                        res.Succes = true;
                        res.Message = "Su reserva fue eliminada correctamente";
                        return res;


                    }
                    throw new InvalidOperationException("No se puede eliminar un detalle de su orden en este momento");
                }

               
                throw new InvalidOperationException("No se puede eliminar una orden cuyo estado no sea creado ");

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

        public async Task<Response> CancelReservation(int idReservation)
        {
            Response res = new Response();
            try
            {
                ReservationModel Reserva = await _reservationRepository.GetByIdReservation(idReservation);
                if (Reserva.Status == Status.Creada)
                {
                   Reserva.Status = Status.Cancelada;
                    await _reservationRepository.EditReservation(Reserva);
                    res.Succes = true;
                    res.Message = "Su reserva fue cancelada correctamente";
                    return res;
                }


                throw new InvalidOperationException("No se puede cancelar una orden cuyo estado no sea creado ");



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

        public async Task<Response> BuyReservation(int idReservation)
        {
            Response res = new Response();
            try
            {
                ReservationModel Reserva = await _reservationRepository.GetByIdReservation(idReservation);
                if (Reserva.Status == Status.Creada)
                {
                    Reserva.Status = Status.Pagada;
                    await _reservationRepository.EditReservation(Reserva);
                    res.Succes = true;
                    res.Message = "Su reserva fue Pagada correctamente";
                    return res;
                }


                throw new InvalidOperationException("No se puede pagar una orden cuyo estado no sea creado ");



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
