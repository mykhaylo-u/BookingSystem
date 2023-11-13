using AutoMapper;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Data;
using BookingSystem.Domain.Models.Theater;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public TheaterRepository(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Theater> AddAsync(Theater theater, CancellationToken cancellationToken)
        {
            var theaterEntity = _mapper.Map<Data.Entities.Theater>(theater);

            await _context.Theaters.AddAsync(theaterEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Theater>(theaterEntity);
        }

        public async Task<Theater?> UpdateAsync(int id, Theater theater)
        {
            var theaterEntity = _mapper.Map<Data.Entities.Theater>(theater);

            var theaterToUpdate = await _context.Theaters.FirstOrDefaultAsync(m => m.Id == id);
            if (theaterToUpdate == null)
            {
                return null;
            }

            theaterToUpdate.Name = theaterEntity.Name;
            theaterToUpdate.TotalSeats = theaterEntity.TotalSeats;

            _context.Entry(theaterToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<Theater>(theaterToUpdate);
        }

        public async Task<Theater?> DeleteAsync(int id)
        {
            var theaterToDelete = await _context.Theaters.FindAsync(id);
            if (theaterToDelete == null)
            {
                return null;
            }

            _context.Theaters.Remove(theaterToDelete);
            await _context.SaveChangesAsync();

            return _mapper.Map<Theater>(theaterToDelete);
        }

        public async Task<IEnumerable<Theater>> GetAllAsync()
        {
            var theaters = await _context.Theaters
                .ToListAsync();

            return theaters.Select(t => _mapper.Map<Theater>(t));
        }

        public async Task<Theater?> GetByIdAsync(int id)
        {
            var theater = await _context.Theaters
                .FirstOrDefaultAsync(t => t.Id == id);

            return _mapper.Map<Theater>(theater);
        }
    }
}
