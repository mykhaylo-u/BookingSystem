using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Queries;
using BookingSystem.Domain.Services.MovieManagement.QueryHandlers;
using Moq;

namespace UnitTests.BookingSystem.Domain.Services.MovieManagement.QueryHandlers
{
    public class GetMovieListQueryHandlerTests
    {
        private readonly List<Movie> _movies = new()
        {
            new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today, DateTime.Today.AddDays(30), "summary1"),
            new Movie("2nd", TimeSpan.FromMinutes(222), "comedy", DateTime.Today.AddDays(2), DateTime.Today.AddDays(15), "summary2")
        };

        [Fact]
        public async Task Handle_ReturnsAvailableMovies()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(_movies);

            var handler = new GetMovieListQueryHandler(movieRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetMovieListQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_movies.Count, result.Count());
            Assert.Equal(_movies, result);
        }

        [Fact]
        public async Task Handle_NoAvailableMovies_ReturnsEmptyCollection()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Movie>());

            var handler = new GetMovieListQueryHandler(movieRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetMovieListQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test case 2: Filters out unavailable movies
        [Fact]
        public async Task Handle_FiltersOutUnavailableMovies_ReturnsOnlyAvailableMovies()
        {
            // Arrange
            var currentDate = DateTime.Now;

            var availableMovies = _movies.Where(m => m.ShowStartDate <= currentDate && m.ShowEndDate >= currentDate);

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(availableMovies);

            var handler = new GetMovieListQueryHandler(movieRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetMovieListQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(availableMovies, result);
        }
    }
}
