using MediatR;

namespace BookingSystem.Domain.Models.Showtime.Queries
{
    public class GetShowTimesListQuery: IRequest<IEnumerable<ShowTime>>
    {
    }
}
