using Moq;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.SeatReservation.Queries;
using BookingSystem.Domain.Services.SeatReservation.QueryHandlers;

namespace UnitTests.BookingSystem.Domain.Services.SeatReservation.QueryHandler
{
    public class GetAvailableSeatsQueryHandlerTests
    {
        private readonly Mock<ISeatReservationRepository> _mockSeatReservationRepository;
        private readonly GetAvailableSeatsQueryHandler _handler;

        public GetAvailableSeatsQueryHandlerTests()
        {
            _mockSeatReservationRepository = new Mock<ISeatReservationRepository>();
            _handler = new GetAvailableSeatsQueryHandler(_mockSeatReservationRepository.Object);
        }

        [Fact]
        public async Task Handle_SeatsAreAvailable_ReturnsAvailableSeats()
        {
            // Arrange
            var showTimeId = 1;
            var availableSeats = new List<Seat>
            {
                new Seat(1, 1),
                new Seat(1, 2),
                new Seat(1, 3, false),
                new Seat(2, 1)
            };

            _mockSeatReservationRepository.Setup(repo => repo.GetAllAvailableAsync(showTimeId))
                .ReturnsAsync(availableSeats.Where(s => s.IsAvailable));

            var query = new GetAvailableSeatsQuery { ShowTimeId = showTimeId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.DoesNotContain(result, s => !s.IsAvailable);
        }

        [Fact]
        public async Task Handle_NoSeatsAreAvailable_ReturnsEmpty()
        {
            // Arrange
            var showTimeId = 1;
            var availableSeats = new List<Seat>();

            _mockSeatReservationRepository.Setup(repo => repo.GetAllAvailableAsync(showTimeId))
                .ReturnsAsync(availableSeats);

            var query = new GetAvailableSeatsQuery { ShowTimeId = showTimeId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_Throws()
        {
            // Arrange
            var showTimeId = 1;

            _mockSeatReservationRepository.Setup(repo => repo.GetAllAvailableAsync(showTimeId))
                .ThrowsAsync(new System.Exception("Database error"));

            var query = new GetAvailableSeatsQuery { ShowTimeId = showTimeId };

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}