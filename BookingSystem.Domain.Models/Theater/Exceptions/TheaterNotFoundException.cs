namespace BookingSystem.Domain.Models.Theater.Exceptions
{
    public class TheaterNotFoundException : DomainValidationException
    {
        public TheaterNotFoundException(int movieId)
            : base($"No Theater found with ID {movieId}.", "TU-010") { }
    }
}
