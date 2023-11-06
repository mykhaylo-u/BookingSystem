using MediatR;

namespace BookingSystem.Domain.Models.Showtime.Commands
{
    public class DeleteShowTimeCommand : IRequest<ShowTime>
    {
        public DeleteShowTimeCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
