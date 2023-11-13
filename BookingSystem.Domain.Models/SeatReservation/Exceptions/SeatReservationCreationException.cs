namespace BookingSystem.Domain.Models.SeatReservation.Exceptions
{
    public class SeatReservationCreationException : DomainValidationException
    {
        public SeatReservationCreationException() : base("Something happen during Seat SeatReservation creation.",
            "SRC-010")
        {
        }
    }
}

