using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.Models.Showtime
{
    public class ShowTime
    {
        /// <summary>
        /// ShowTime ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movie ID.
        /// </summary>
        [Required]
        public int MovieId { get; set; }

        /// <summary>
        /// Theater ID.
        /// </summary>
        [Required]
        public int TheaterId { get; set; }

        /// <summary>
        /// Movie start date..
        /// </summary>
        /// <example>2023-12-24T14:00:00Z</example>
        [Required]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Movie end date.
        /// </summary>
        /// <example>2023-12-24T16:00:00Z</example>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Ticket price.
        /// </summary>
        /// <example>120</example>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TicketPrice { get; set; }

        /// <summary>
        /// List of seats.
        /// </summary>
        public List<SeatReservation.Seat> Seats { get; set; }

        public Movie.Movie? Movie { get; set; }
        public Theater.Theater? Theater { get; set; }

        public ShowTime(int movieId, int theaterId, DateTime startDateTime, DateTime endDateTime, decimal ticketPrice, List<SeatReservation.Seat> seats)
        {
            MovieId = movieId;
            TheaterId = theaterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TicketPrice = ticketPrice;
            Seats = seats;
        }
    }
}
