using MediatR;

namespace BookingSystem.Domain.Models.Theater.Commands
{
    public class UpdateTheaterCommand : IRequest<Theater>
    {
        public int Id { get; set; }
        public string Name { get; }
        public int TotalSeats { get; }

        public UpdateTheaterCommand(int id, string name, int totalSeats)
        {
            Id = id;
            Name = name;
            TotalSeats = totalSeats;
        }
    }
}
