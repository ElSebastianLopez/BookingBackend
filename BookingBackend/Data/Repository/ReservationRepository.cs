using BookingBackend.Data.Repository.IRepository;
using BookingBackend.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingBackend.Data.Repository
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly ContextModel _contextModel;
        public ReservationRepository(ContextModel contextModel)
        {

            _contextModel = contextModel;

        }

        public async Task<bool> CreateReservation(ReservationModel reservation)
        {
            await _contextModel.Reservation.AddAsync(reservation);
            return await Save();
        }

        public async Task<bool> DeleteReservation(ReservationModel reservation)
        {
            _contextModel.Reservation.Remove(reservation);
            return await Save();
        }

        public async Task<bool> EditReservation(ReservationModel reservation)
        {
            _contextModel.Reservation.Update(reservation);
            return await Save();
        }

        public async Task<List<ReservationModel>> GetAllReservation(Expression<Func<ReservationModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _contextModel.Reservation.Include(m=>m.Customer).ToListAsync();
            }
            return await _contextModel.Reservation.Include(m => m.Customer).Where(predicate).ToListAsync();
        }

        public async Task<ReservationModel> GetByIdReservation(int id)
        {
            return await _contextModel.Reservation.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _contextModel.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
