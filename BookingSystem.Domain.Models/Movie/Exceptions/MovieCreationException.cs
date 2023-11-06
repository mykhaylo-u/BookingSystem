namespace BookingSystem.Domain.Models.Movie.Exceptions
{
    public class MovieCreationException : DomainValidationException
    {
        public MovieCreationException() : base("Something happen during Movie creation.", "MU -001")
        {
        }
    }
}
