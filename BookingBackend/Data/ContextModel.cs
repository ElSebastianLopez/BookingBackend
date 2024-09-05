using BookingBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingBackend.Data
{
    public class ContextModel : DbContext
    {
        public ContextModel(DbContextOptions<ContextModel> options)
           : base(options)
        {

        }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<ServiceModel> Service { get; set; }
        public DbSet<ReservationModel> Reservation { get; set; }
        public DbSet<ReservationDetailModel> ReservationDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
