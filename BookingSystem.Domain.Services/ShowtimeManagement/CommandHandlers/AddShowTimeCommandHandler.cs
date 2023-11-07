using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Exceptions;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using BookingSystem.Domain.Models.Theater.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.ShowtimeManagement.CommandHandlers
{
    public class AddShowTimeCommandHandler : IRequestHandler<AddShowTimeCommand, ShowTime>
    {
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ITheaterRepository _theaterRepository;
        private readonly ILogger<AddShowTimeCommandHandler> _logger;

        public AddShowTimeCommandHandler(IShowTimeRepository showTimeRepository, IMovieRepository movieRepository, ITheaterRepository theaterRepository, ILogger<AddShowTimeCommandHandler> logger)
        {
            _showTimeRepository = showTimeRepository;
            _movieRepository = movieRepository;
            _theaterRepository = theaterRepository;
            _logger = logger;
        }

        public async Task<ShowTime> Handle(AddShowTimeCommand request, CancellationToken cancellationToken)
        {
            if (request.StartDateTime.Date < DateTime.Now.Date)
            {
                throw new ShowTimeCreationException();
            }

            var movie = await _movieRepository.GetByIdAsync(request.MovieId);
            if (movie == null)
            {
                throw new MovieNotFoundException(request.MovieId);
            }

            var theater = await _theaterRepository.GetByIdAsync(request.TheaterId);
            if (theater == null)
            {
                throw new TheaterNotFoundException(request.TheaterId);
            }

            var addedShowTime = await _showTimeRepository.AddAsync(new ShowTime(request.MovieId, request.TheaterId, request.StartDateTime,
                request.EndDateTime, request.TicketPrice, request.Seats));

            _logger.LogInformation("New ShowTime created.");

            return addedShowTime ?? throw new ShowTimeCreationException();
        }
    }
}
