using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.TheaterManagement.QueryHandlers
{
    public class GetTheaterByIdQueryHandler : IRequestHandler<GetTheaterByIdQuery, Theater?>
    {
        private readonly ITheaterRepository _theaterRepository;

        public GetTheaterByIdQueryHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<Theater?> Handle(GetTheaterByIdQuery request, CancellationToken cancellationToken)
        {
            return await _theaterRepository.GetByIdAsync(request.Id);
        }
    }
}
