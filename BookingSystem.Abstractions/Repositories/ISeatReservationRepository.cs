using BookingSystem.Domain.Models.SeatReservation;

namespace BookingSystem.Abstractions.Repositories
{
    public interface ISeatReservationRepository
    {
        Task<IEnumerable<SeatReservation>> GetAllAsync();
        Task<IEnumerable<Seat>> GetAllAvailableAsync(int showTimeId);
        //Task<IEnumerable<SeatReservation>> GetConfirmedReservationsAsync();
        Task<SeatReservation> AddAsync(SeatReservation seatReservation);
        Task<BookingConfirmation> AddBookingConfirmationAsync(BookingConfirmation bookingConfirmation);
        //Task<SeatReservation?> UpdateAsync(int id, SeatReservation seatReservation);
        //Task<SeatReservation?> DeleteAsync(int id);
        Task<SeatReservation?> GetByIdAsync(int id);
    }
}
