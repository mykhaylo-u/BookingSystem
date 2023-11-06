using BookingSystem.Domain.Models.SeatReservation;

namespace BookingSystem.Api.ViewModels.SeatReservation.Responses
{
    /// <summary>
    /// A ViewModel representing a movie.
    /// </summary>
    public class SeatReservationViewModel
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Showtime associated with the reservation.
        /// </summary>
        public string ShowtimeId { get; set; }

        /// <summary>
        /// User for whom the reservation is made.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// List of reserved seat IDs.
        /// </summary>
        public List<Seat> ReservedSeats { get; set; }

        /// <summary>
        /// Reservation time.
        /// </summary>
        public string ReservationTime { get; set; }


        /// <summary>
        /// Total price for reservation.
        /// </summary>
        public string TotalPrice { get; set; }

    }
}
