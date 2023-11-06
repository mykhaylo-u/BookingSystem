using MediatR;

namespace BookingSystem.Domain.Models.Movie.Commands
{
    public class DeleteMovieCommand : IRequest<Movie>
    {
        public DeleteMovieCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
