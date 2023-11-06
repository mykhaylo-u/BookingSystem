using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Queries
{
    public class GetAllSeatReservationsQuery : IRequest<IEnumerable<SeatReservation>>
    {
    }
}
