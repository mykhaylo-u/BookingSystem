namespace BookingSystem.Domain.Models.Movie.Exceptions
{
    public class MovieNotFoundException : DomainValidationException
    {
        public MovieNotFoundException(int movieId)
            : base($"No movie found with ID {movieId}.", "MU-010") { }
    }
}
