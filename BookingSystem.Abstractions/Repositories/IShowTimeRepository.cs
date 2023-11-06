using BookingSystem.Domain.Models.Showtime;

namespace BookingSystem.Abstractions.Repositories
{
    public interface IShowTimeRepository
    {
        Task<IEnumerable<ShowTime>> GetAllAsync();
        Task<ShowTime> AddAsync(ShowTime showTime);
        Task<ShowTime?> UpdateAsync(int id, ShowTime showTime);
        Task<ShowTime?> DeleteAsync(int id);
        Task<ShowTime?> GetByIdAsync(int id);
    }
}
