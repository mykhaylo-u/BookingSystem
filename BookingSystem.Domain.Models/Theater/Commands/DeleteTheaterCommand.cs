using MediatR;

namespace BookingSystem.Domain.Models.Theater.Commands
{
    public class DeleteTheaterCommand : IRequest<Theater>
    {
        public DeleteTheaterCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
