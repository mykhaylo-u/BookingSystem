﻿namespace BookingSystem.Domain.Models.Theater.Exceptions
{
    public class TheaterCreationException : DomainValidationException
    {
        public TheaterCreationException() : base("Something happen during Theater creation.", "TU-001")
        {
        }
    }
}
