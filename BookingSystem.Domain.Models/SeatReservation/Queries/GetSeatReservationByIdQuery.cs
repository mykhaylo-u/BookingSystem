using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Queries
{
    public class GetSeatReservationByIdQuery : IRequest<SeatReservation?>
    {
        public int Id { get; }

        public GetSeatReservationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
