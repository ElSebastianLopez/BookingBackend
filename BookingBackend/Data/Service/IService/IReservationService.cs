using ApiBook.Models.DTOs;
using BookingBackend.Model.DTO;

namespace BookingBackend.Data.Service.IService
{
    public interface IReservationService
    {
        Task<Response> GetAllReservationByIdCustomer(int id);
        Task<Response> GetByIdReservation(int id);
        Task<Response> AddOrEditReservation(ReservationDTO reservationDTO);
        Task<Response> DeleteReservation(int id);
        Task<Response> DeleteReservationDet(int idReservation, int idReservationDet);
        Task<Response> CancelReservation(int idReservation);
        Task<Response> BuyReservation(int idReservation);
    }
}
