using MediatR;

namespace BookingSystem.Domain.Models.Showtime.Queries
{
    public class GetShowTimeByIdQuery : IRequest<ShowTime?>
    {
        public int Id { get; }

        public GetShowTimeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
