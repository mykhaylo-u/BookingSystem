using MediatR;

namespace BookingSystem.Domain.Models.Theater.Commands
{
    public class AddTheaterCommand : IRequest<Theater>
    {
        public string Name { get; }
        public int TotalSeats { get; }

        public AddTheaterCommand(string name, int totalSeats)
        {
            Name = name;
            TotalSeats = totalSeats;
        }
    }
}
