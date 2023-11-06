namespace BookingSystem.Domain.Models.SeatReservation.Exceptions
{
    public class BookingConfirmationException : DomainValidationException
    {
        public BookingConfirmationException() : base("Something happen during Seat Booking Confirmation .", "BC-010")
        {
        }
    }
}

