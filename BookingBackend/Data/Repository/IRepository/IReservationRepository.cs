using BookingBackend.Model;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository.IRepository
{
    public interface IReservationRepository
    {
        Task<bool> CreateReservation(ReservationModel reservation);
        Task<bool> DeleteReservation(ReservationModel reservation);
        Task<bool> EditReservation(ReservationModel reservation);
        Task<List<ReservationModel>> GetAllReservation(Expression<Func<ReservationModel, bool>> predicate = null);
        Task<ReservationModel> GetByIdReservation(int id);
        Task<bool> Save();
    }
}
