using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.SeatReservation.QueryHandlers
{
    public class GetAvailableSeatsQueryHandler : IRequestHandler<GetAvailableSeatsQuery, IEnumerable<Models.SeatReservation.Seat>>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;

        public GetAvailableSeatsQueryHandler(ISeatReservationRepository seatReservationRepository)
        {
            _seatReservationRepository = seatReservationRepository;
        }

        public async Task<IEnumerable<Models.SeatReservation.Seat>> Handle(GetAvailableSeatsQuery request, CancellationToken cancellationToken)
        {
            return await _seatReservationRepository.GetAllAvailableAsync(request.ShowTimeId);
        }
    }
}
