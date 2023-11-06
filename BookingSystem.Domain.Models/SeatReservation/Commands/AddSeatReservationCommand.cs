using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Commands
{
    public class AddSeatReservationCommand : IRequest<SeatReservation>
    {
        public int ShowtimeId { get; set; }
        public string UserId { get; set; }
        public List<int> ReservedSeatIds { get; set; }

        public AddSeatReservationCommand(int showtimeId, string userId, List<int> reservedSeatIds)
        {
            ShowtimeId = showtimeId;
            UserId = userId;
            ReservedSeatIds = reservedSeatIds;
        }

    }
}
