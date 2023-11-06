using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.MovieManagement.QueryHandlers
{
    public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, IEnumerable<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieListQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> Handle(GetMovieListQuery request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetAllAsync();
        }
    }
}
