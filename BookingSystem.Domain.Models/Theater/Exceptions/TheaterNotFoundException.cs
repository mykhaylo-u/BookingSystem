namespace BookingSystem.Domain.Models.Theater.Exceptions
{
    public class TheaterNotFoundException : DomainValidationException
    {
        public TheaterNotFoundException(int theaterId)
            : base($"No Theater found with ID {theaterId}.", "TU-010") { }
    }
}
