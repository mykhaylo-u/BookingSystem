using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Api.ViewModels.SeatReservation.Request
{
    /// <summary>
    /// Represents a request to create a new SeatReservation.
    /// </summary>
    public class AddSeatReservationRequest
    {
        /// <summary>
        /// Showtime to which the reservation applies.
        /// </summary>
        /// <value>1</value>
        [Required]
        public int ShowtimeId { get; set; }

        /// <summary>
        /// User making the reservation.
        /// </summary>
        /// <value>1</value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// List of seat user try to reserve.
        /// </summary>
        /// <value>[1,2]</value>
        [Required]
        public List<int> ReservedSeatIds { get; set; }
    }
}
