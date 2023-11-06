namespace BookingSystem.Domain.Models
{
    public class DomainValidationException : Exception
    {
        public string ErrorCode { get; private set; }

        public DomainValidationException() { }

        public DomainValidationException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
