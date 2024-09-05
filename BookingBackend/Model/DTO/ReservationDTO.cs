using System.ComponentModel.DataAnnotations.Schema;

namespace BookingBackend.Model.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

     
        public int IdCustomer { get; set; }
       

        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public DateTime ReservationDate { get; set; }

        public string TotalReserveCost { get; set; }

        public List<ReservationDetailDTO> Details { get; set; }=new List<ReservationDetailDTO>();

        
    }
}
