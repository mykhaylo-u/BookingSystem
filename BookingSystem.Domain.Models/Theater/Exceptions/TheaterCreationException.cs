namespace BookingSystem.Domain.Models.Theater.Exceptions
{
    public class TheaterCreationException : DomainValidationException
    {
        public TheaterCreationException() : base("Show Start Date could not be before today.", "MU -001")
        {
        }
    }
}
