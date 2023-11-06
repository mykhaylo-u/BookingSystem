using BookingSystem.Abstractions;
using System.Linq.Expressions;

namespace BookingSystem.Domain.Specifications.Movie
{
    public class AvailableMoviesSpecification : ISpecification<Models.Movie.Movie>
    {
        private readonly DateTime _currentDate;

        public AvailableMoviesSpecification(DateTime currentDate)
        {
            _currentDate = currentDate;
        }

        public Expression<Func<Models.Movie.Movie, bool>> Criteria => movie =>
            movie.ShowStartDate <= _currentDate && movie.ShowEndDate >= _currentDate;
    }
}
