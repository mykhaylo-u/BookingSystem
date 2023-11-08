using MediatR;

namespace BookingSystem.Domain.Models.Theater.Queries
{
    public class GetTheaterByIdQuery : IRequest<Theater?>
    {
        public GetTheaterByIdQuery(int id)
        {
            Id = id;
        }

        public GetTheaterByIdQuery()
        {
        }

        public int Id { get; set; }
    }
}
