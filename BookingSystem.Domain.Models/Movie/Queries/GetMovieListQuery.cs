using MediatR;

namespace BookingSystem.Domain.Models.Movie.Queries
{
    public class GetMovieListQuery : IRequest<IEnumerable<Movie>>
    {
    }
}
