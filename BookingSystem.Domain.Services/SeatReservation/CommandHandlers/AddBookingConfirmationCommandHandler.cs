using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using BookingSystem.Domain.Models.Showtime.Exceptions;
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
            var reservation = await _seatReservationRepository.GetByIdAsync(request.ReservationId);
            
            if (reservation == null || reservation.ReservationEndDate < DateTime.Now)
            {
                throw new BookingConfirmationTimeOutException();
            }

            if (reservation.IsConfirmed)
            {
                throw new BookingConfirmationDuplicationException();
            }

            if (reservation.Showtime == null)
            {
                throw new BookingConfirmationException();
            }

            var totalPrice = reservation.Showtime.TicketPrice * reservation.ReservedSeats.Count;

            var addedSeatReservation = await _seatReservationRepository.AddBookingConfirmationAsync(
                new BookingConfirmation(request.ReservationId, totalPrice));

            _logger.LogInformation("New BookingConfirmation created.");

            return addedSeatReservation ?? throw new BookingConfirmationException();
        }
    }
}
