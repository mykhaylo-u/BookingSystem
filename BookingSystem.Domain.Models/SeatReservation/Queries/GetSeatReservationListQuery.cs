using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Queries
{
    public class GetSeatReservationListQuery : IRequest<IEnumerable<SeatReservation>>
    {
    }
}
