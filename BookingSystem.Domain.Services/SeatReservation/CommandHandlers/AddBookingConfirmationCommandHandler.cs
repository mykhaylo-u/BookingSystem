using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.SeatReservation.CommandHandlers
{
    public class AddBookingConfirmationCommandHandler : IRequestHandler<AddBookingConfirmationCommand, BookingConfirmation>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;
        private readonly ILogger<AddBookingConfirmationCommandHandler> _logger;

        public AddBookingConfirmationCommandHandler(ISeatReservationRepository seatReservationRepository, ILogger<AddBookingConfirmationCommandHandler> logger)
        {
            _seatReservationRepository = seatReservationRepository;
            _logger = logger;
        }

        public async Task<BookingConfirmation> Handle(AddBookingConfirmationCommand request, CancellationToken cancellationToken)
        {
            var addedSeatReservation = await _seatReservationRepository.AddBookingConfirmationAsync(
                new BookingConfirmation(request.ReservationId));

            _logger.LogInformation("New BookingConfirmation created.");

            return addedSeatReservation ?? throw new BookingConfirmationException();
        }
    }
}
