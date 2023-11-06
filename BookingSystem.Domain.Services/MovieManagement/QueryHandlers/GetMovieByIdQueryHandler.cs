using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.MovieManagement.QueryHandlers
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie?>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id);
            return movie;
        }
    }
}
