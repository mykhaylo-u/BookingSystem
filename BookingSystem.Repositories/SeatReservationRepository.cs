using AutoMapper;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories
{
    public class SeatReservationRepository : ISeatReservationRepository
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public SeatReservationRepository(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeatReservation>> GetAllAsync()
        {
            var seatReservationEntities = await _context.SeatReservations.Include(sr => sr.ReservedSeats).ToListAsync();
            return seatReservationEntities.Select(e => _mapper.Map<SeatReservation>(e));
        }

        public async Task<IEnumerable<Seat>> GetAllAvailableAsync(int showTimeId)
        {
            var availableSeatEntities = await _context.ShowTimes
                .Include(sr => sr.Seats)
                .Where(st => st.Id == showTimeId)
                .SelectMany(st => st.Seats)
                .Where(st => st.IsAvailable)
                .ToListAsync();

            var reservedSeats = await _context.SeatReservations
                .Where(sr => sr.ShowtimeId == showTimeId && sr.ReservationEndDate > DateTime.Now)
                .SelectMany(sr => sr.ReservedSeats.Select(s => s.Id))
                .ToListAsync();

            return availableSeatEntities
                .Where(s => !reservedSeats.Contains(s.Id))
                .Select(e => _mapper.Map<Seat>(e));
        }

        public async Task<SeatReservation> AddSeatReservationAsync(SeatReservation seatSeatReservation)
        {
            var seatReservationEntity = _mapper.Map<Data.Entities.SeatReservation>(seatSeatReservation);

            var seats = await _context.Seats.Where(s => seatSeatReservation.ReservedSeatsIds.Contains(s.Id))
                .ToListAsync();

            seatReservationEntity.ReservedSeats = seats;

            _context.SeatReservations.Add(seatReservationEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<SeatReservation>(seatReservationEntity); ;
        }

        public async Task<BookingConfirmation> AddBookingConfirmationAsync(BookingConfirmation bookingConfirmation)
        {
            var confirmationEntity = _mapper.Map<Data.Entities.BookingConfirmation>(bookingConfirmation);

            var seatReservation = await _context.SeatReservations
                .Include(sr => sr.ReservedSeats)
                .Include(seatReservation => seatReservation.Showtime)
                .FirstAsync(sr => sr.Id == bookingConfirmation.SeatReservationId);

            confirmationEntity.TotalPrice = bookingConfirmation.TotalPrice;

            foreach (var seat in seatReservation.ReservedSeats)
            {
                seat.IsAvailable = false;
            }

            seatReservation.IsConfirmed = true;

            _context.BookingConfirmations.Add(confirmationEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingConfirmation>(confirmationEntity);
        }

        public async Task<SeatReservation?> DeleteAsync(int id)
        {
            var seatReservationToDelete = await _context.SeatReservations.FindAsync(id);
            if (seatReservationToDelete == null)
            {
                return null;
            }

            _context.SeatReservations.Remove(seatReservationToDelete);
            await _context.SaveChangesAsync();

            return _mapper.Map<SeatReservation>(seatReservationToDelete);
        }

        public async Task<SeatReservation?> GetByIdAsync(int id)
        {
            var seatReservationEntity = await _context.SeatReservations
                .Include(sr => sr.ReservedSeats)
                .Include(sr => sr.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<SeatReservation>(seatReservationEntity);
        }
    }
}
