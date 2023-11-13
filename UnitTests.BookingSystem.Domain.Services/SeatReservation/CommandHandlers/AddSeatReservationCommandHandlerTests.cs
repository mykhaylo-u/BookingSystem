using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using BookingSystem.Domain.Services.SeatReservation.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Threading.Tasks;
using BookingSystemDomain = BookingSystem.Domain.Models;
using EndOfStreamException = System.IO.EndOfStreamException;

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

        [Fact]
        public async Task ConcurrentSeatReservation_Attempt_ShouldFailForOneUser()
        {
            // Arrange
            var showtimeId = 1;
            var userId1 = "1";
            var userId2 = "2";
            var userId3 = "3";
            var userId4 = "4";
            var reservedSeatIds = new List<int> { 5, 7 };

            var availableSeats = reservedSeatIds.Select(id => new Seat(1, 2) { Id = id }).ToList();

            _mockSeatReservationRepository.Setup(repo => repo.GetAllAvailableAsync(showtimeId)).ReturnsAsync(availableSeats);

            _mockSeatReservationRepository
                .Setup(repo => repo.AddSeatReservationAsync(It.IsAny<BookingSystemDomain.SeatReservation.SeatReservation>()))
                .Callback<BookingSystemDomain.SeatReservation.SeatReservation>(reservation =>
                {
                    availableSeats.RemoveAll(seat => reservation.ReservedSeatsIds.Contains(seat.Id));
                })
                .ReturnsAsync((BookingSystemDomain.SeatReservation.SeatReservation reservation) => reservation);

            var command1 = new AddSeatReservationCommand(showtimeId, userId1, reservedSeatIds);
            var command2 = new AddSeatReservationCommand(showtimeId, userId2, reservedSeatIds);
            var command3 = new AddSeatReservationCommand(showtimeId, userId3, reservedSeatIds);
            var command4 = new AddSeatReservationCommand(showtimeId, userId4, reservedSeatIds);

            // Act & Assert
            var task1 = Task.Run(() => _handler.Handle(command1, CancellationToken.None));
            var task2 = Task.Run(() => _handler.Handle(command2, CancellationToken.None));
            var task3 = Task.Run(() => _handler.Handle(command3, CancellationToken.None));
            var task4 = Task.Run(() => _handler.Handle(command4, CancellationToken.None));

            var tasks = new[] { task1, task2, task3, task4 };

            try
            {
                await Task.WhenAll(task1, task2, task3, task4);
            }
            catch (Exception ex)
            {
                // ignored
            }

            var res = tasks.Where(t => t.IsCompletedSuccessfully).Select(t => t).ToList();
            Assert.True(res.Count == 1);
            Assert.NotNull(res.First().Result);

            var exceptions = tasks.Where(t => t.Exception != null).Select(t => t.Exception).ToList();

            var seatReservationUnavailableExceptionOccurred = exceptions
                .All(e => e.InnerExceptions.All(e => e is SeatReservationUnavailableException));
            Assert.True(seatReservationUnavailableExceptionOccurred, "Expected All SeatReservationUnavailableException");
        }
    }
}
