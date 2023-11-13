namespace BookingSystem.Domain.Models.Showtime.Exceptions
{
    public class InvalidShowTimeDateRangeException : DomainValidationException
    {
        public InvalidShowTimeDateRangeException(DateTime startDate, DateTime endDate)
            : base($"The showtime date range from {startDate} to {endDate} is invalid.", "ST-002")
        {
        }

        public InvalidShowTimeDateRangeException(DateTime startDate)
            : base($"The showtime Start date {startDate} is invalid.", "ST-003") { }
    }
}
