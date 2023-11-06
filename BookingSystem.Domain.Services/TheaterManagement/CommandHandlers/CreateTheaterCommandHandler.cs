using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater;
using MediatR;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Seat;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class CreateTheaterCommandHandler : IRequestHandler<CreateTheaterCommand, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly ILogger<CreateTheaterCommandHandler> _logger;

        public CreateTheaterCommandHandler(ITheaterRepository theaterRepository, ILogger<CreateTheaterCommandHandler> logger)
        {
            _theaterRepository = theaterRepository;
            _logger = logger;
        }

        public async Task<Theater> Handle(CreateTheaterCommand request, CancellationToken cancellationToken)
        {
            var theater = new Theater(request.Name, request.TotalSeats);

            //foreach (var seat in request.Seats)
            //{
            //    theater.Seats.Add(new Seat(seat.Row, seat.Number));
            //}

            var addedTheater = await _theaterRepository.AddAsync(theater, cancellationToken);

            _logger.LogInformation("New theater created.");

            return addedTheater;
        }
    }
}
