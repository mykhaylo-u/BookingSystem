using BookingSystem.Api.ViewModels.Theater.Responses;

namespace BookingSystem.Api.ViewModels.ShowTime
{
    public class ShowTimeViewModel
    {
        /// <summary>
        /// ShowTime ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Movie ID.
        /// </summary>
        public string MovieId { get; set; }

        /// <summary>
        /// Movie Name.
        /// </summary>
        public string MovieName { get; set; }

        /// <summary>
        /// Theater ID.
        /// </summary>
        public string TheaterId { get; set; }

        /// <summary>
        /// Theater Name.
        /// </summary>
        public string TheaterName { get; set; }

        /// <summary>
        /// Movie start date.
        /// </summary>
        public string StartDateTime { get; set; }

        /// <summary>
        /// Movie end date.
        /// </summary>
        public string EndDateTime { get; set; }

        /// <summary>
        /// Ticket price.
        /// </summary>
        public string TicketPrice { get; set; }

        /// <summary>
        /// List of seats.
        /// </summary>
        public List<SeatViewModel> Seats { get; set; }
    }
}
