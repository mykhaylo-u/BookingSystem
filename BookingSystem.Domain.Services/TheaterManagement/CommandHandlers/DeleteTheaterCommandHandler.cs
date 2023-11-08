using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class DeleteTheaterCommandHandler : IRequestHandler<DeleteTheaterCommand, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly ILogger<DeleteTheaterCommandHandler> _logger;

        public DeleteTheaterCommandHandler(ITheaterRepository theaterRepository, ILogger<DeleteTheaterCommandHandler> logger)
        {
            _theaterRepository = theaterRepository;
            _logger = logger;
        }

        public async Task<Theater> Handle(DeleteTheaterCommand request, CancellationToken cancellationToken)
        {

            var deletedTheater = await _theaterRepository.DeleteAsync(request.Id);

            _logger.LogInformation("Theater was deleted.");

            return deletedTheater ?? throw new TheaterNotFoundException(request.Id);
        }
    }
}
