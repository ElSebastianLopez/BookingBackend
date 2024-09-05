using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingBackend.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Nit { get; set; }    

        public string CellPhone { get; set; }

        public string Email {  get; set; }

        public string Password { get; set; }

    }
}
