using System.ComponentModel.DataAnnotations;
using BookingSystem.Domain.Models.SeatReservation;

namespace BookingSystem.Api.ViewModels.ShowTime.Request
{
    /// <summary>
    /// Represents a request to add a new showtime.
    /// </summary>
    public class AddShowTimeRequest
    {
        /// <summary>
        /// Movie ID for the showtime.
        /// </summary>
        /// <example>1</example>
        [Required]
        public int MovieId { get; set; }

        /// <summary>
        /// Theater ID where the showtime will take place.
        /// </summary>
        /// <example>1</example>
        [Required]
        public int TheaterId { get; set; }

        /// <summary>
        /// Start date for the showtime.
        /// </summary>
        ///  <example>2023-11-25</example>
        [Required]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// End date for the showtime.
        /// </summary>
        ///  <example>2023-11-29</example>
        [Required]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Ticket price for the showtime.
        /// </summary>
        ///  <example>120</example>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Ticket price must be a positive number.")]
        public decimal TicketPrice { get; set; }

        /// <summary>
        /// The list of seats in the theater.
        /// </summary>
        /// <example>[{ "row": 1, "number": 1, "isAvailable": true },{ "row": 1, "number": 2, "isAvailable": true },{ "row": 1, "number": 3, "isAvailable": true } ,{ "row": 1, "number": 4, "isAvailable": true },{ "row": 1, "number": 5, "isAvailable": true }]</example>
        public List<Seat> Seats { get; set; }
    }

}
