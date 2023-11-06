using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Queries
{
    public class GetAvailableSeatsQuery : IRequest<IEnumerable<Seat>>
    {
        public GetAvailableSeatsQuery(int showTimeId)
        {
            ShowTimeId = showTimeId;
        }

        public int ShowTimeId { get; set; }
    }
}
