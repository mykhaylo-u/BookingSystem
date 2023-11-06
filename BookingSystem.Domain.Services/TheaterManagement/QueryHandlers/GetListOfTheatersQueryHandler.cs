using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Queries;
using MediatR;

namespace BookingSystem.Domain.Services.TheaterManagement.QueryHandlers
{
    public class GetListOfTheatersQueryHandler : IRequestHandler<GetListOfTheatersQuery, IEnumerable<Theater>>
    {
        private readonly ITheaterRepository _theaterRepository;

        public GetListOfTheatersQueryHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<IEnumerable<Theater>> Handle(GetListOfTheatersQuery request, CancellationToken cancellationToken)
        {
            return await _theaterRepository.GetList();
        }
    }
}
