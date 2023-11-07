namespace BookingSystem.Domain.Models.SeatReservation
{
    public class BookingConfirmation
    {
        public int Id { get; set; }
        public int SeatReservationId { get; set; }
        public decimal TotalPrice { get; set; }

        public BookingConfirmation(int seatReservationId, decimal totalPrice)
        {
            SeatReservationId = seatReservationId;
            TotalPrice = totalPrice;
        }
    }
}
