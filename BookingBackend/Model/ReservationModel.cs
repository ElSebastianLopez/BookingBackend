using System.ComponentModel.DataAnnotations.Schema;
using static BookingBackend.Model.Enums;

namespace BookingBackend.Model
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Customer")]
        public int IdCustomer {  get; set; }
        public virtual CustomerModel Customer { get; set; }

        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public DateTime ReservationDate { get; set; }

        public string TotalReserveCost {  get; set; }
        public Status Status { get; set; }
    }
}
