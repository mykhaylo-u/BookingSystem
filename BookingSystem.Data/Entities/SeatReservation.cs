using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Entities
{
    public class SeatReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ShowTime")]
        public int ShowtimeId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual List<Seat> ReservedSeats { get; set; } = new List<Seat>();

        [Required]
        public DateTime ReservationStartDate { get; set; }

        [Required]
        public DateTime ReservationEndDate { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        public virtual ShowTime Showtime { get; set; }
    }
}
