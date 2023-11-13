using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.SeatReservation.CommandHandlers
{
    public class
        AddSeatReservationCommandHandler : IRequestHandler<AddSeatReservationCommand,
            Models.SeatReservation.SeatReservation>
    {
        private readonly ISeatReservationRepository _seatReservationRepository;
        private readonly ILogger<AddSeatReservationCommandHandler> _logger;

        private static readonly SemaphoreSlim
            Semaphore = new(1, 1); // Initial count is 1, meaning only one thread can enter at a time

        public AddSeatReservationCommandHandler(ISeatReservationRepository seatReservationRepository,
            ILogger<AddSeatReservationCommandHandler> logger)
        {
            _seatReservationRepository = seatReservationRepository;
            _logger = logger;
        }

        public async Task<Models.SeatReservation.SeatReservation> Handle(AddSeatReservationCommand request,
            CancellationToken cancellationToken)
        {
            await Semaphore.WaitAsync(cancellationToken);

            try
            {
                var availableSeats = await _seatReservationRepository.GetAllAvailableAsync(request.ShowtimeId);


                if (!request.ReservedSeatIds.All(rs => availableSeats.Select(s => s.Id).Contains(rs)))
                {
                    throw new SeatReservationUnavailableException();
                }

                var dateTimeNow = DateTime.Now;

                var addedSeatReservation = await _seatReservationRepository.AddSeatReservationAsync(
                    new Models.SeatReservation.SeatReservation(request.ShowtimeId, request.UserId,
                        request.ReservedSeatIds)
                    {
                        ReservationStartDate = dateTimeNow,
                        ReservationEndDate =
                            dateTimeNow.AddMinutes(Utilities.Constants.ReservationTimeOut)
                    });

                if (addedSeatReservation == null)
                {
                    throw new SeatReservationCreationException();
                }

                _logger.LogInformation("New SeatReservation created.");

                return addedSeatReservation;
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}
