using System.ComponentModel.DataAnnotations.Schema;
using static BookingBackend.Model.Enums;

namespace BookingBackend.Model
{
    public class ReservationDetailModel
    {
        public int Id { get; set; }

        [ForeignKey("Service")]
        public int IdService { get; set; }
        public virtual ServiceModel Service { get; set; }

        [ForeignKey("Reservation")]
        public int IdReservation { get; set; }
        public virtual ReservationModel Reservation { get; set; }

        public decimal ServiceCost { get; set; }

    }
}
