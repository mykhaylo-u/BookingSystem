using BookingSystem.Domain.Models.Movie;

namespace BookingSystem.Abstractions.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<IEnumerable<Movie>> GetAvailableMoviesAsync();
        Task<Movie> AddAsync(Movie movie);
        Task<Movie?> UpdateAsync(int id, Movie movie);
        Task<Movie?> DeleteAsync(int id);
        Task<Movie?> GetByIdAsync(int id);
    }
}
