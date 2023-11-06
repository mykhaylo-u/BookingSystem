namespace BookingSystem.Domain.Models.Movie.Exceptions
{
    public class MovieCreationException : DomainValidationException
    {
        public MovieCreationException() : base("Show Start Date could not be before today.", "MU -001")
        {
        }
    }
}
