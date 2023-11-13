using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater;
using MediatR;
using BookingSystem.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using BookingSystem.Domain.Models.Theater.Exceptions;

namespace BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class UpdateTheaterCommandHandler : IRequestHandler<UpdateTheaterCommand, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly ILogger<UpdateTheaterCommandHandler> _logger;

        public UpdateTheaterCommandHandler(ITheaterRepository theaterRepository, ILogger<UpdateTheaterCommandHandler> logger)
        {
            _theaterRepository = theaterRepository;
            _logger = logger;
        }

        public async Task<Theater> Handle(UpdateTheaterCommand request, CancellationToken cancellationToken)
        {
            var addedTheater = await _theaterRepository.UpdateAsync(request.Id, new Theater(request.Name, request.TotalSeats));

            _logger.LogInformation($"Theater ID: {request.Id} was updated.");

            return addedTheater ?? throw new TheaterNotFoundException(request.Id);
        }
    }
}
