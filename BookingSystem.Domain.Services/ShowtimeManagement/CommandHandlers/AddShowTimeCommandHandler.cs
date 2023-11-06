using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.ShowtimeManagement.CommandHandlers
{
    public class AddShowTimeCommandHandler : IRequestHandler<AddShowTimeCommand, ShowTime>
    {
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly ILogger<AddShowTimeCommandHandler> _logger;

        public AddShowTimeCommandHandler(IShowTimeRepository showTimeRepository, ILogger<AddShowTimeCommandHandler> logger)
        {
            _showTimeRepository = showTimeRepository;
            _logger = logger;
        }

        public async Task<ShowTime> Handle(AddShowTimeCommand request, CancellationToken cancellationToken)
        {
            if (request.StartDateTime.Date < DateTime.Now.Date)
            {
                throw new ShowTimeCreationException();
            }

            var addedShowTime = await _showTimeRepository.AddAsync(new ShowTime(request.MovieId, request.TheaterId, request.StartDateTime,
                request.EndDateTime, request.TicketPrice, request.Seats));

            _logger.LogInformation("New ShowTime created.");

            return addedShowTime ?? throw new ShowTimeCreationException();
        }
    }
}
