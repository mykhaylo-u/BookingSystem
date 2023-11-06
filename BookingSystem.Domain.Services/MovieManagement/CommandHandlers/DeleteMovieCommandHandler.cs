using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Domain.Services.MovieManagement.CommandHandlers
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<DeleteMovieCommandHandler> _logger;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository, ILogger<DeleteMovieCommandHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task<Movie> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            
            var deletedMovie = await _movieRepository.DeleteAsync(request.Id);

            _logger.LogInformation("Movie was deleted.");

            return deletedMovie ?? throw new MovieCreationException();
        }
    }
}
