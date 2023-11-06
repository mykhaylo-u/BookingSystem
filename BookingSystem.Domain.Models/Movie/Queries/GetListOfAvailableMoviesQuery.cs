using MediatR;

namespace BookingSystem.Domain.Models.Movie.Queries
{
    public class GetListOfAvailableMoviesQuery : IRequest<IEnumerable<Movie>>
    {
    }
}
