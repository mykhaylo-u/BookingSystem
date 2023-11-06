namespace BookingSystem.Api.ViewModels.Theater.Responses
{
    public class SeatViewModel
    {
        /// <summary>
        /// Seat ID.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Row number.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Seat number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Indicates whether the seat is available.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
