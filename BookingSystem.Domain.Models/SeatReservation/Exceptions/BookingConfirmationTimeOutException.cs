namespace BookingSystem.Domain.Models.SeatReservation.Exceptions
{
    public class BookingConfirmationTimeOutException : DomainValidationException
    {
        public BookingConfirmationTimeOutException() : base("Seat Booking you are trying to confirm is not active anymore.", "BC-030")
        {
        }
    }
}

