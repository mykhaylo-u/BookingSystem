using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater;
using MediatR;
using BookingSystem.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using BookingSystem.Domain.Models.Theater.Exceptions;

namespace BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class AddTheaterCommandHandler : IRequestHandler<AddTheaterCommand, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly ILogger<AddTheaterCommandHandler> _logger;

        public AddTheaterCommandHandler(ITheaterRepository theaterRepository, ILogger<AddTheaterCommandHandler> logger)
        {
            _theaterRepository = theaterRepository;
            _logger = logger;
        }

        public async Task<Theater> Handle(AddTheaterCommand request, CancellationToken cancellationToken)
        {
            var theater = new Theater(request.Name, request.TotalSeats);

            var addedTheater = await _theaterRepository.AddAsync(theater, cancellationToken);

            _logger.LogInformation("New theater created.");

            return addedTheater ?? throw new TheaterCreationException();
        }
    }
}
