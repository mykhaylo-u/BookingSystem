using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using BookingSystem.Domain.Services.SeatReservation.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystemDomain = BookingSystem.Domain.Models;

namespace UnitTests.BookingSystem.Domain.Services.SeatReservation.CommandHandlers
{
    public class AddSeatReservationCommandHandlerTests
    {
        private readonly Mock<ISeatReservationRepository> _mockSeatReservationRepository;
        private readonly Mock<ILogger<AddSeatReservationCommandHandler>> _mockLogger;
        private readonly AddSeatReservationCommandHandler _handler;

        public AddSeatReservationCommandHandlerTests()
        {
            _mockSeatReservationRepository = new Mock<ISeatReservationRepository>();
            _mockLogger = new Mock<ILogger<AddSeatReservationCommandHandler>>();
            _handler = new AddSeatReservationCommandHandler(_mockSeatReservationRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_AllSeatsAvailable_ShouldCreateReservation()
        {
            // Arrange
            var command = new AddSeatReservationCommand(1, "testUser", new List<int>() { 1, 2 });

            var availableSeats = new[] { new Seat(1, 1) { Id = 1 }, new Seat(1, 2) { Id = 2 } };

            _mockSeatReservationRepository.Setup(x => x.GetAllAvailableAsync(It.IsAny<int>()))
                .ReturnsAsync(availableSeats);

            _mockSeatReservationRepository.Setup(x => x.AddSeatReservationAsync(It.IsAny<BookingSystemDomain.SeatReservation.SeatReservation>()))
                .ReturnsAsync((BookingSystemDomain.SeatReservation.SeatReservation reservation) => reservation);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockSeatReservationRepository.Verify(x => x.AddSeatReservationAsync(It.IsAny<BookingSystemDomain.SeatReservation.SeatReservation>()), Times.Once);
        }

        [Fact]
        public async Task Handle_SeatNotAvailable_ShouldThrowException()
        {
            // Arrange
            var command = new AddSeatReservationCommand(1, "testUser", new List<int>() { 1, 2, 3 });

            var availableSeats = new[] { new Seat(1, 1) { Id = 1 }, new Seat(1, 2) { Id = 2 } }; ;

            _mockSeatReservationRepository.Setup(x => x.GetAllAvailableAsync(It.IsAny<int>()))
                .ReturnsAsync(availableSeats);

            // Act & Assert
            await Assert.ThrowsAsync<SeatReservationUnavailableException>(() => _handler.Handle(command, CancellationToken.None));
            _mockSeatReservationRepository.Verify(x => x.AddSeatReservationAsync(It.IsAny<BookingSystemDomain.SeatReservation.SeatReservation>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ReservationCreationFails_ShouldThrowException()
        {
            // Arrange
            var command = new AddSeatReservationCommand(1, "testUser", new List<int>() { 1, 2 });

            var availableSeats = new[] { new Seat(1, 1) { Id = 1 }, new Seat(1, 2) { Id = 2 } };

            _mockSeatReservationRepository.Setup(x => x.GetAllAvailableAsync(It.IsAny<int>()))
                .ReturnsAsync(availableSeats);

            _mockSeatReservationRepository.Setup(x => x.AddSeatReservationAsync(It.IsAny<BookingSystemDomain.SeatReservation.SeatReservation>()))
                .ReturnsAsync((BookingSystemDomain.SeatReservation.SeatReservation)null);

            // Act & Assert
            await Assert.ThrowsAsync<SeatReservationCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var command = new AddSeatReservationCommand(1, "testUser", new List<int>() { 1, 2 });

            var availableSeats = new[] { new Seat(1, 1), new Seat(1, 2) };

            _mockSeatReservationRepository.Setup(x => x.GetAllAvailableAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
