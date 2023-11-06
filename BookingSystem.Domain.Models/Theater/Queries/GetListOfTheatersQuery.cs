using MediatR;

namespace BookingSystem.Domain.Models.Theater.Queries
{
    public class GetListOfTheatersQuery : IRequest<IEnumerable<Theater>>
    {
    }
}
