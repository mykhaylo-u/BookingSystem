namespace BookingSystem.Domain.Models.SeatReservation.Exceptions
{
    public class BookingConfirmationDuplicationException : DomainValidationException
    {
        public BookingConfirmationDuplicationException() : base("Seat Booking you are trying to confirm is already confirmed .", "BC-020")
        {
        }
    }
}

