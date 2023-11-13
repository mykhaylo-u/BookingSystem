using BookingSystem.Domain.Models.Showtime;

namespace BookingSystem.Domain.Models.SeatReservation
{
    public class SeatReservation
    {
        public int Id { get; set; }
        public int ShowtimeId { get; set; }
        public string UserId { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
        public List<Seat> ReservedSeats { get; set; } = new();
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public bool IsConfirmed { get; set; }
        public ShowTime? Showtime { get; set; }

        public SeatReservation(int showtimeId, string userId, List<int> reservedSeatsIds)
        {
            ShowtimeId = showtimeId;
            UserId = userId;
            ReservedSeatsIds = reservedSeatsIds;
        }

        public SeatReservation()
        {

        }
    }
}
