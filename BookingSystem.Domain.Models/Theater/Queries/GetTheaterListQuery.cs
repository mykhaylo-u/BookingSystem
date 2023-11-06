using MediatR;

namespace BookingSystem.Domain.Models.Theater.Queries
{
    public class GetTheaterListQuery : IRequest<IEnumerable<Theater>>
    {
    }
}
