using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.SeatReservation.CommandHandlers
{
    public class AddSeatReservationCommandHandler : IRequestHandler<AddSeatReservationCommand, Models.SeatReservation.SeatReservation>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;
        private readonly ILogger<AddSeatReservationCommandHandler> _logger;

        public AddSeatReservationCommandHandler(ISeatReservationRepository seatReservationRepository, ILogger<AddSeatReservationCommandHandler> logger)
        {
            _seatReservationRepository = seatReservationRepository;
            _logger = logger;
        }

        public async Task<Models.SeatReservation.SeatReservation> Handle(AddSeatReservationCommand request, CancellationToken cancellationToken)
        {
            var dateTimeNow = DateTime.Now;
            var addedSeatReservation = await _seatReservationRepository.AddAsync(new Models.SeatReservation.SeatReservation(request.ShowtimeId, request.UserId, request.ReservedSeatIds)
            {
                ReservationStartDate = dateTimeNow,
                ReservationEndDate = dateTimeNow.AddMinutes(BookingSystem.Utilities.Constants.ReservationTimeOut)
            });

            _logger.LogInformation("New SeatReservation created.");

            return addedSeatReservation ?? throw new SeatReservationCreationException();
        }
    }
}
