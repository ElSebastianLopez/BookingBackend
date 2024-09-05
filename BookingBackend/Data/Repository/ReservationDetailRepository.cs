using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository
{
    public class ReservationDetailRepository: IReservationDetailRepository
    {
        private readonly ContextModel _contextModel;
        public ReservationDetailRepository(ContextModel contextModel)
        {

            _contextModel = contextModel;

        }

        public async Task<bool> CreateReservationDetail(ReservationDetailModel reservationDetail)
        {
            await _contextModel.ReservationDetail.AddAsync(reservationDetail);
            return await Save();
        }

        public async Task<bool> DeleteReservationDetail(ReservationDetailModel reservationDetail)
        {
            _contextModel.ReservationDetail.Remove(reservationDetail);
            return await Save();
        }

        public async Task<bool> EditReservationDetail(ReservationDetailModel reservationDetail)
        {
            _contextModel.ReservationDetail.Update(reservationDetail);
            return await Save();
        }

        public async Task<List<ReservationDetailModel>> GetAllReservationDetail(Expression<Func<ReservationDetailModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _contextModel.ReservationDetail.ToListAsync();
            }
            return await _contextModel.ReservationDetail.Where(predicate).ToListAsync();
        }

        public async Task<ReservationDetailModel> GetByIdReservationDetail(int id)
        {
            return await _contextModel.ReservationDetail.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _contextModel.SaveChangesAsync() > 0 ? true : false;
        }

    }
}
