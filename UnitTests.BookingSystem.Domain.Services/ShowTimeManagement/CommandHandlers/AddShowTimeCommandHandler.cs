using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Movie.Exceptions;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Exceptions;
using BookingSystem.Domain.Models.Theater.Exceptions;
using BookingSystem.Domain.Services.ShowtimeManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Theater;

namespace UnitTests.BookingSystem.Domain.Services.ShowTimeManagement.CommandHandlers
{
    public class AddShowTimeCommandHandlerTests
    {
        private readonly Mock<IShowTimeRepository> _mockShowTimeRepository;
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly Mock<ILogger<AddShowTimeCommandHandler>> _mockLogger;
        private readonly AddShowTimeCommandHandler _handler;

        public AddShowTimeCommandHandlerTests()
        {
            _mockShowTimeRepository = new Mock<IShowTimeRepository>();
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _mockLogger = new Mock<ILogger<AddShowTimeCommandHandler>>();
            _handler = new AddShowTimeCommandHandler(_mockShowTimeRepository.Object, _mockMovieRepository.Object, _mockTheaterRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_WithPastStartDateTime_ThrowsShowTimeCreationException()
        {
            // Arrange
            var command = new AddShowTimeCommand
            {
                StartDateTime = DateTime.Now.AddDays(-1)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ShowTimeCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNonExistentMovie_ThrowsMovieNotFoundException()
        {
            // Arrange
            var command = new AddShowTimeCommand
            {
                MovieId = 500,
                StartDateTime = DateTime.Now.AddDays(1)
            };

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(command.MovieId)).ReturnsAsync((Movie)null);

            // Act & Assert
            await Assert.ThrowsAsync<MovieNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNonExistentTheater_ThrowsTheaterNotFoundException()
        {
            // Arrange
            var command = new AddShowTimeCommand
            {
                MovieId = 1,
                TheaterId = 500,
                StartDateTime = DateTime.Now.AddDays(1)
            };

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(command.MovieId)).ReturnsAsync(new Movie());
            _mockTheaterRepository.Setup(repo => repo.GetByIdAsync(command.TheaterId)).ReturnsAsync((Theater)null);

            // Act & Assert
            await Assert.ThrowsAsync<TheaterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithValidData_AddsShowTimeSuccessfully()
        {
            // Arrange
            var command = new AddShowTimeCommand
            {
                MovieId = 1,
                TheaterId = 2,
                StartDateTime = DateTime.Now.AddDays(1),
                EndDateTime = DateTime.Now.AddDays(1).AddHours(2),
                TicketPrice = 100,
                Seats = new List<Seat>()
            };

            var movie = new Movie();
            var theater = new Theater();

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(command.MovieId)).ReturnsAsync(movie);
            _mockTheaterRepository.Setup(repo => repo.GetByIdAsync(command.TheaterId)).ReturnsAsync(theater);

            _mockShowTimeRepository.Setup(repo => repo.AddAsync(It.IsAny<ShowTime>())).ReturnsAsync(new ShowTime());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockShowTimeRepository.Verify(repo => repo.AddAsync(It.IsAny<ShowTime>()), Times.Once);
        }
    }
}
