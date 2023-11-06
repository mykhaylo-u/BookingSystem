using AutoMapper;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Data;
using BookingSystem.Domain.Models.Showtime;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public ShowTimeRepository(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowTime>> GetAllAsync()
        {
            var showTimeEntities = await _context.ShowTimes
                .Include(sh => sh.Movie)
                .Include(sh => sh.Theater)
                .Include(sh => sh.Seats)
                .ToListAsync();
            return showTimeEntities.Select(e => _mapper.Map<ShowTime>(e));
        }

        public async Task<ShowTime> AddAsync(ShowTime showTime)
        {
            var showTimeEntity = _mapper.Map<Data.Entities.ShowTime>(showTime);

            _context.ShowTimes.Add(showTimeEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShowTime>(showTimeEntity); ;
        }

        public async Task<ShowTime?> UpdateAsync(int id, ShowTime showTime)
        {
            var showTimeEntity = _mapper.Map<Data.Entities.ShowTime>(showTime);

            var showTimeToUpdate = await _context.ShowTimes
                .Include(sh => sh.Seats)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showTimeToUpdate == null)
            {
                return null;
            }

            showTimeToUpdate.StartDateTime = showTimeEntity.StartDateTime;
            showTimeToUpdate.EndDateTime = showTimeEntity.EndDateTime;
            showTimeToUpdate.TicketPrice = showTimeEntity.TicketPrice;
            showTimeToUpdate.TheaterId = showTimeEntity.TheaterId;
            showTimeToUpdate.MovieId = showTimeEntity.MovieId;
            showTimeToUpdate.Seats = showTimeEntity.Seats;

            _context.Entry(showTimeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return _mapper.Map<ShowTime>(showTimeToUpdate);
        }

        public async Task<ShowTime?> DeleteAsync(int id)
        {
            var showTimeToDelete = await _context.ShowTimes.FindAsync(id);
            if (showTimeToDelete == null)
            {
                return null;
            }

            _context.ShowTimes.Remove(showTimeToDelete);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShowTime>(showTimeToDelete);
        }

        public async Task<ShowTime?> GetByIdAsync(int id)
        {
            var showTimeEntity = await _context.ShowTimes.Include(sh => sh.Movie).Include(sh => sh.Theater).Include(sh => sh.Seats)
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ShowTime>(showTimeEntity);
        }
    }
}
