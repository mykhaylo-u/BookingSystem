using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Theater;

namespace BookingSystem.Abstractions.Repositories
{
    public interface ITheaterRepository
    {
        Task<Theater> AddAsync(Theater theater, CancellationToken cancellationToken);
        Task<Theater?> UpdateAsync(int id, Theater theater);
        Task<Theater?> DeleteAsync(int id);
        Task<IEnumerable<Theater>> GetAllAsync();
        Task<Theater> GetByIdAsync(int id);
    }
}
