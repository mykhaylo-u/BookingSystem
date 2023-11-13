namespace BookingSystem.Domain.Models.Showtime.Exceptions
{
    public class ShowTimeNotFoundException : DomainValidationException
    {
        public ShowTimeNotFoundException(int showTimeId)
            : base($"No showtime found with ID {showTimeId}.", "ST-010") { }
    }
}
