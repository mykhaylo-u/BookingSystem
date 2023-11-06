﻿namespace BookingSystem.Domain.Models.Showtime.Exceptions
{
    public class ShowTimeCreationException : DomainValidationException
    {
        public ShowTimeCreationException() : base("Something happen during ShowTime creation.", "STC-010")
        {
        }
    }
}
