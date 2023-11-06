using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.MovieManagement.QueryHandlers
{
    public class GetListOfAvailableMoviesQueryHandler : IRequestHandler<GetListOfAvailableMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetListOfAvailableMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> Handle(GetListOfAvailableMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetAvailableMoviesAsync();
        }
    }
}
