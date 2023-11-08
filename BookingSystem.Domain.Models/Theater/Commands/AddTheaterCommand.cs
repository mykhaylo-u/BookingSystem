using MediatR;

namespace BookingSystem.Domain.Models.Theater.Commands
{
    public class AddTheaterCommand : IRequest<Theater>
    {
        public string Name { get; set; }
        public int TotalSeats { get; set; }

        public AddTheaterCommand(string name, int totalSeats)
        {
            Name = name;
            TotalSeats = totalSeats;
        }

        public AddTheaterCommand()
        {
        }
    }
}
