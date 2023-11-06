using MediatR;

namespace BookingSystem.Domain.Models.Theater.Commands
{
    public class CreateTheaterCommand : IRequest<Theater>
    {
        public string Name { get; }
        public int TotalSeats { get; }

        public CreateTheaterCommand(string name, int totalSeats)
        {
            Name = name;
            TotalSeats = totalSeats;
        }
    }
}
