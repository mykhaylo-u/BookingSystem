using MediatR;

namespace BookingSystem.Domain.Models.Showtime.Commands
{
    public class AddShowTimeCommand : IRequest<ShowTime>
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public List<SeatReservation.Seat> Seats { get; set; }

        public AddShowTimeCommand(int movieId, int theaterId, DateTime startDateTime, DateTime endDateTime, decimal ticketPrice, List<SeatReservation.Seat> seats)
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
