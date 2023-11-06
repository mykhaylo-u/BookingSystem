using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.SeatReservation.QueryHandlers
{
    public class GetSeatReservationByIdQueryHandler : IRequestHandler<GetSeatReservationByIdQuery, Models.SeatReservation.SeatReservation?>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;

        public GetSeatReservationByIdQueryHandler(ISeatReservationRepository seatReservationRepository)
        {
            _seatReservationRepository = seatReservationRepository;
        }

        public async Task<Models.SeatReservation.SeatReservation?> Handle(GetSeatReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _seatReservationRepository.GetByIdAsync(request.Id);
            return movie;
        }
    }
}
