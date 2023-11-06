using BookingSystem.Domain.Models.SeatReservation;

namespace BookingSystem.Abstractions.Repositories
{
    public interface ISeatReservationRepository
    {
        Task<IEnumerable<SeatReservation>> GetAllAsync();
        Task<IEnumerable<Seat>> GetAllAvailableAsync(int showTimeId);
        Task<SeatReservation> AddSeatReservationAsync(SeatReservation seatReservation);
        Task<BookingConfirmation> AddBookingConfirmationAsync(BookingConfirmation bookingConfirmation);
        Task<SeatReservation?> GetByIdAsync(int id);
    }
}
