using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.MovieManagement.CommandHandlers
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<UpdateMovieCommandHandler> _logger;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository, ILogger<UpdateMovieCommandHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.ShowStartDate.Date < DateTime.Now.Date)
            {
                throw new InvalidMovieDateRangeException(request.ShowStartDate.Date);
            }

            var updatedMovie = await _movieRepository.UpdateAsync(request.Id, new Movie(request.Title, request.Duration, request.Genre,
                request.ShowStartDate, request.ShowEndDate, request.Summary));

            _logger.LogInformation($"Movie ID: {updatedMovie?.Id} was updated.");

            return updatedMovie ?? throw new MovieNotFoundException(request.Id);
        }
    }
}
