using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Queries
{
    public class GetAvailableSeatsQuery : IRequest<IEnumerable<Seat>>
    {
        public GetAvailableSeatsQuery(int showTimeId)
        {
            ShowTimeId = showTimeId;
        }

        public GetAvailableSeatsQuery()
        {
        }

        public int ShowTimeId { get; set; }
    }
}
