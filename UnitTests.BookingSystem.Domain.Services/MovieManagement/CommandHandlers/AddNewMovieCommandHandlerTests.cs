using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie.Exceptions;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Services.MovieManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models;

namespace UnitTests.BookingSystem.Domain.Services.MovieManagement.CommandHandlers
{
    public class AddNewMovieCommandHandlerTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<ILogger<AddNewMovieCommandHandler>> _loggerMock;
        private readonly AddNewMovieCommandHandler _handler;

        public AddNewMovieCommandHandlerTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _loggerMock = new Mock<ILogger<AddNewMovieCommandHandler>>();
            _handler = new AddNewMovieCommandHandler(_movieRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_SuccessfullyAddsMovie_ReturnsAddedMovie()
        {
            // Arrange
            var movie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today,
                DateTime.Today.AddDays(30), "summary1");
            _movieRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Movie>()))
                .ReturnsAsync(movie);

            // Act
            var result = await _handler.Handle(new AddNewMovieCommand(movie.Title, movie.Duration, movie.Genre, movie.Summary, movie.ShowStartDate, movie.ShowEndDate), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movie, result);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var movie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today,
                DateTime.Today.AddDays(30), "summary1");
            _movieRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Movie>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new AddNewMovieCommand(movie.Title, movie.Duration, movie.Genre, movie.Summary, movie.ShowStartDate, movie.ShowEndDate), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NullMovie_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            await Assert.ThrowsAsync<MovieCreationException>(() =>
                _handler.Handle(new AddNewMovieCommand(null, TimeSpan.MinValue, null, null, DateTime.Now, DateTime.Now),
                    CancellationToken.None));
        }
    }
}
