using AutoMapper;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(BookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movieEntities = await _context.Movies.ToListAsync();
            return movieEntities.Select(e => _mapper.Map<Movie>(e));
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            var movieEntity = _mapper.Map<Data.Entities.Movie>(movie);

            _context.Movies.Add(movieEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Movie>(movieEntity); ;
        }

        public async Task<Movie?> UpdateAsync(int id, Movie movie)
        {
            var movieEntity = _mapper.Map<Data.Entities.Movie>(movie);

            var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movieToUpdate == null)
            {
                return null;
            }

            movieToUpdate.Title = movieEntity.Title;
            movieToUpdate.Duration = movieEntity.Duration;
            movieToUpdate.Genre = movieEntity.Genre;
            movieToUpdate.Summary = movieEntity.Summary;
            movieToUpdate.ShowStartDate = movieEntity.ShowStartDate;
            movieToUpdate.ShowEndDate = movieEntity.ShowEndDate;

            _context.Entry(movieToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<Movie>(movieToUpdate);
        }

        public async Task<Movie?> DeleteAsync(int id)
        {
            var movieToDelete = await _context.Movies.FindAsync(id);
            if (movieToDelete == null)
            {
                return null;
            }

            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
            return _mapper.Map<Movie>(movieToDelete);
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            var movieEntity = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<Movie>(movieEntity);
        }
    }
}
