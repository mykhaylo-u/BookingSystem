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
        /// <example>1</example>
        [Required]
        public int ShowtimeId { get; set; }

        /// <summary>
        /// User making the reservation.
        /// </summary>
        /// <example>1</example>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// List of seat user try to reserve.
        /// </summary>
        /// <example>[1,2]</example>
        [Required]
        public List<int> ReservedSeatIds { get; set; }
    }
}
