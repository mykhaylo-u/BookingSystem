namespace BookingSystem.Domain.Models.SeatReservation.Exceptions
{
    public class SeatReservationUnavailableException : DomainValidationException
    {
        public SeatReservationUnavailableException() : base("Some seats you are trying to reserve is already taken.", "SRC-020")
        {
        }
    }
}

