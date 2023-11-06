using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.ShowtimeManagement.CommandHandlers
{
    public class UpdateShowTimeCommandHandler : IRequestHandler<UpdateShowTimeCommand, ShowTime>
    {
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly ILogger<UpdateShowTimeCommandHandler> _logger;

        public UpdateShowTimeCommandHandler(IShowTimeRepository showTimeRepository, ILogger<UpdateShowTimeCommandHandler> logger)
        {
            _showTimeRepository = showTimeRepository;
            _logger = logger;
        }

        public async Task<ShowTime> Handle(UpdateShowTimeCommand request, CancellationToken cancellationToken)
        {
            if (request.StartDateTime.Date < DateTime.Now.Date)
            {
                throw new ShowTimeCreationException();
            }

            var addedShowTime = await _showTimeRepository.UpdateAsync(request.Id,new ShowTime(request.MovieId, request.TheaterId, request.StartDateTime,
                request.EndDateTime, request.TicketPrice, request.Seats));

            _logger.LogInformation("ShowTime was updated.");

            return addedShowTime ?? throw new ShowTimeCreationException();
        }
    }
}
