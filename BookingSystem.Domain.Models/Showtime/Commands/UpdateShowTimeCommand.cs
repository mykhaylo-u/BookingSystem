using MediatR;

namespace BookingSystem.Domain.Models.Showtime.Commands
{
    public class UpdateShowTimeCommand : IRequest<ShowTime>
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public List<SeatReservation.Seat> Seats { get; set; }

        public UpdateShowTimeCommand(int id, int movieId, int theaterId, DateTime startDateTime, DateTime endDateTime, decimal ticketPrice, List<SeatReservation.Seat> seats)
        {
            Id = id;
            MovieId = movieId;
            TheaterId = theaterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TicketPrice = ticketPrice;
            Seats = seats;
        }

        public UpdateShowTimeCommand()
        {
        }
    }
}
