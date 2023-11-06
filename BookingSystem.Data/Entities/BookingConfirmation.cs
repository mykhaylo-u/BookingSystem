using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Entities
{
    public class BookingConfirmation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SeatReservationId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        public virtual SeatReservation SeatReservation { get; set; }
    }
}
