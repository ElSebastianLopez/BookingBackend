using BookingBackend.Model;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository.IRepository
{
    public interface IReservationDetailRepository
    {
        Task<bool> CreateReservationDetail(ReservationDetailModel reservationDetail);
        Task<bool> DeleteReservationDetail(ReservationDetailModel reservationDetail);
        Task<bool> EditReservationDetail(ReservationDetailModel reservationDetail);
        Task<List<ReservationDetailModel>> GetAllReservationDetail(Expression<Func<ReservationDetailModel, bool>> predicate = null);
        Task<ReservationDetailModel> GetByIdReservationDetail(int id);
        Task<bool> Save();
    }
}
