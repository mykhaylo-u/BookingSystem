namespace BookingSystem.Api.ViewModels.SeatReservation.Responses
{
    /// <summary>
    /// A ViewModel representing a Seat.
    /// </summary>
    public class SeatViewModel
    {
        /// <summary>
        /// Row number of the seat.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Seat number within the row.
        /// </summary>
        public int Number { get; set; }

    }
}
