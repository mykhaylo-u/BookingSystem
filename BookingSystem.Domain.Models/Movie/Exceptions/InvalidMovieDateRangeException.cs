namespace BookingSystem.Domain.Models.Movie.Exceptions
{
    public class InvalidMovieDateRangeException : DomainValidationException
    {
        public InvalidMovieDateRangeException(DateTime startDate, DateTime endDate)
            : base($"The movie date range from {startDate} to {endDate} is invalid.", "MC-002") { }

        public InvalidMovieDateRangeException(DateTime startDate)
            : base($"The movie Start date {startDate} is invalid.", "MC-003") { }
    }
}
