using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Queries;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Services.MovieManagement.QueryHandlers;
using Moq;

namespace UnitTests.BookingSystem.Domain.Services.MovieManagement.QueryHandlers
{
    public class GetMovieByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_MovieExists_ReturnsMovie()
        {
            // Arrange
            var movieId = 1;
            var expectedMovie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today,
                DateTime.Today.AddDays(30), "summary1");

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(expectedMovie);

            var handler = new GetMovieByIdQueryHandler(movieRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetMovieByIdQuery(movieId), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMovie, result);
        }

        [Fact]
        public async Task Handle_MovieDoesNotExist_ReturnsNull()
        {
            // Arrange
            var movieId = 1;

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync((Movie)null);

            var handler = new GetMovieByIdQueryHandler(movieRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetMovieByIdQuery(movieId), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var movieId = 1;

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId))
                .ThrowsAsync(new Exception("Database error"));

            var handler = new GetMovieByIdQueryHandler(movieRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(new GetMovieByIdQuery(movieId), CancellationToken.None));
        }
    }
}
