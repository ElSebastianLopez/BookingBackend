using System.ComponentModel.DataAnnotations.Schema;

namespace BookingBackend.Model.DTO
{
    public class ReservationDetailDTO
    {
        public int? Id { get; set; }

        public int IdService { get; set; }
        public string? service { get; set; }

        public int? IdReservation { get; set; }

        public decimal ServiceCost { get; set; }
    }
}
