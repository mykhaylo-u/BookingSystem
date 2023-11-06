using MediatR;

namespace BookingSystem.Domain.Models.Movie.Queries
{
    public class GetMovieByIdQuery : IRequest<Movie?>
    {
        public int Id { get; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
