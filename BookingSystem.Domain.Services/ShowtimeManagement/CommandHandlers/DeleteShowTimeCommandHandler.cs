using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.ShowTimeManagement.CommandHandlers
{
    public class DeleteShowTimeCommandHandler : IRequestHandler<DeleteShowTimeCommand, ShowTime>
    {
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly ILogger<DeleteShowTimeCommandHandler> _logger;

        public DeleteShowTimeCommandHandler(IShowTimeRepository showTimeRepository, ILogger<DeleteShowTimeCommandHandler> logger)
        {
            _showTimeRepository = showTimeRepository;
            _logger = logger;
        }

        public async Task<ShowTime> Handle(DeleteShowTimeCommand request, CancellationToken cancellationToken)
        {

            var deletedShowTime = await _showTimeRepository.DeleteAsync(request.Id);

            _logger.LogInformation("ShowTime was deleted.");

            return deletedShowTime ?? throw new ShowTimeNotFoundException(request.Id);
        }
    }
}
