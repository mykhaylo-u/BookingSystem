namespace BookingSystem.Domain.Models.SeatReservation
{
    public class Seat
    {
        /// <summary>
        /// Seat ID.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Row number of the seat.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Seat number within the row.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Indicates if the seat is available.
        /// </summary>
        public bool IsAvailable { get; set; }

        public Seat(int row, int number, bool isAvailable = true)
        {
            Row = row;
            Number = number;
            IsAvailable = isAvailable;
        }
    }
}
