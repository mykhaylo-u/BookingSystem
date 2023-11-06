using MediatR;

namespace BookingSystem.Domain.Models.SeatReservation.Commands
{
    public class AddBookingConfirmationCommand : IRequest<BookingConfirmation>
    {
        public int ReservationId { get; set; }

        public AddBookingConfirmationCommand(int reservationId)
        {
            ReservationId = reservationId;
        }

    }
}
