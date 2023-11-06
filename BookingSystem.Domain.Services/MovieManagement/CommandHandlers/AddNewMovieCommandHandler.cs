using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.MovieManagement.CommandHandlers
{
    public class AddNewMovieCommandHandler : IRequestHandler<AddNewMovieCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<AddNewMovieCommandHandler> _logger;

        public AddNewMovieCommandHandler(IMovieRepository movieRepository, ILogger<AddNewMovieCommandHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task<Movie> Handle(AddNewMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.ShowStartDate.Date < DateTime.Now.Date)
            {
                throw new MovieCreationException();
            }

            var addedMovie = await _movieRepository.AddAsync(new Movie(request.Title, request.Duration, request.Genre,
                request.ShowStartDate, request.ShowEndDate, request.Summary));

            _logger.LogInformation("New movie created.");

            return addedMovie ?? throw new MovieCreationException();
        }
    }
}
