using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.ShowtimeManagement.QueryHandlers
{
    public class GetShowTimesListQueryHandler : IRequestHandler<GetShowTimesListQuery, IEnumerable<ShowTime>>
    {
        private readonly IShowTimeRepository _showTimeRepository;

        public GetShowTimesListQueryHandler(IShowTimeRepository showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }

        public async Task<IEnumerable<ShowTime>> Handle(GetShowTimesListQuery request, CancellationToken cancellationToken)
        {
            return await _showTimeRepository.GetAllAsync();
        }
    }
}
