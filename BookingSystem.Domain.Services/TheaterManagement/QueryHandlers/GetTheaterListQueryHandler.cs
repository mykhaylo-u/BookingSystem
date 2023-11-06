using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.TheaterManagement.QueryHandlers
{
    public class GetTheaterListQueryHandler : IRequestHandler<GetTheaterListQuery, IEnumerable<Theater>>
    {
        private readonly ITheaterRepository _theaterRepository;

        public GetTheaterListQueryHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<IEnumerable<Theater>> Handle(GetTheaterListQuery request, CancellationToken cancellationToken)
        {
            return await _theaterRepository.GetAllAsync();
        }
    }
}
