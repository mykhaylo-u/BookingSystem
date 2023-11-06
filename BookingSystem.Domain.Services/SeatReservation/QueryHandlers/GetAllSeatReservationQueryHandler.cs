using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.SeatReservation.QueryHandlers
{
    public class GetAllSeatReservationQueryHandler : IRequestHandler<GetAllSeatReservationsQuery, IEnumerable<Models.SeatReservation.SeatReservation>>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;

        public GetAllSeatReservationQueryHandler(ISeatReservationRepository seatReservationRepository)
        {
            _seatReservationRepository = seatReservationRepository;
        }

        public async Task<IEnumerable<Models.SeatReservation.SeatReservation>> Handle(GetAllSeatReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _seatReservationRepository.GetAllAsync();
        }
    }
}
