using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.ShowTimeManagement.QueryHandlers
{
    public class GetShowTimeByIdQueryHandler : IRequestHandler<GetShowTimeByIdQuery, ShowTime?>
    {
        private readonly IShowTimeRepository _showTimeRepository;

        public GetShowTimeByIdQueryHandler(IShowTimeRepository showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }

        public async Task<ShowTime?> Handle(GetShowTimeByIdQuery request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeRepository.GetByIdAsync(request.Id);
            return showTime;
        }
    }
}
