using BookingSystem.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.SeatReservation;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Exceptions;
using BookingSystem.Domain.Services.SeatReservation.CommandHandlers;
using BookingSystemDomain = BookingSystem.Domain.Models;

namespace UnitTests.BookingSystem.Domain.Services.SeatReservation.CommandHandlers
{
    public class AddBookingConfirmationCommandHandlerTests
    {
        private readonly Mock<ISeatReservationRepository> _mockSeatReservationRepository;
        private readonly Mock<ILogger<AddBookingConfirmationCommandHandler>> _mockLogger;
        private readonly AddBookingConfirmationCommandHandler _handler;

        public AddBookingConfirmationCommandHandlerTests()
        {
            _mockSeatReservationRepository = new Mock<ISeatReservationRepository>();
            _mockLogger = new Mock<ILogger<AddBookingConfirmationCommandHandler>>();
            _handler = new AddBookingConfirmationCommandHandler(_mockSeatReservationRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_SuccessfulBookingConfirmation_ReturnsConfirmation()
        {
            // Arrange
            var reservationId = 1;
            var ticketPrice = 10m;
            var showtime = new BookingSystemDomain.Showtime.ShowTime { TicketPrice = ticketPrice };
            var reservedSeats = new List<Seat> { new Seat { IsAvailable = true }, new Seat { IsAvailable = true } };
            var seatReservation = new BookingSystemDomain.SeatReservation.SeatReservation
            {
                Id = reservationId,
                Showtime = showtime,
                ReservedSeats = reservedSeats,
                ReservationEndDate = DateTime.Now.AddMinutes(30)
            };

            _mockSeatReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(seatReservation);
            _mockSeatReservationRepository.Setup(repo => repo.AddBookingConfirmationAsync(It.IsAny<BookingConfirmation>()))
                .ReturnsAsync(new BookingConfirmation(reservationId, ticketPrice * reservedSeats.Count));

            var command = new AddBookingConfirmationCommand { ReservationId = reservationId };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ticketPrice * reservedSeats.Count, result.TotalPrice);
        }

        [Fact]
        public async Task Handle_ReservationNotFound_ThrowsException()
        {
            // Arrange
            var reservationId = 1;
            _mockSeatReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync((BookingSystemDomain.SeatReservation.SeatReservation)null);

            var command = new AddBookingConfirmationCommand { ReservationId = reservationId };

            // Act & Assert
            await Assert.ThrowsAsync<BookingConfirmationTimeOutException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ReservationTimeout_ThrowsException()
        {
            // Arrange
            var reservationId = 1;
            var seatReservation = new BookingSystemDomain.SeatReservation.SeatReservation
            {
                ReservationEndDate = DateTime.Now.AddMinutes(-1)
            };

            _mockSeatReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(seatReservation);

            var command = new AddBookingConfirmationCommand { ReservationId = reservationId };

            // Act & Assert
            await Assert.ThrowsAsync<BookingConfirmationTimeOutException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ReservationAlreadyConfirmed_ThrowsException()
        {
            // Arrange
            var reservationId = 1;
            var seatReservation = new BookingSystemDomain.SeatReservation.SeatReservation
            {
                IsConfirmed = true,
                ReservationEndDate = DateTime.Now.AddMinutes(30)
            };

            _mockSeatReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(seatReservation);

            var command = new AddBookingConfirmationCommand { ReservationId = reservationId };

            // Act & Assert
            await Assert.ThrowsAsync<BookingConfirmationDuplicationException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_DatabaseSaveFails_ThrowsException()
        {
            // Arrange
            var reservationId = 1;
            var ticketPrice = 10m;
            var showtime = new BookingSystemDomain.Showtime.ShowTime { TicketPrice = ticketPrice };
            var reservedSeats = new List<Seat> { new Seat { IsAvailable = true }, new Seat { IsAvailable = true } };
            var seatReservation = new BookingSystemDomain.SeatReservation.SeatReservation
            {
                Id = reservationId,
                Showtime = showtime,
                ReservedSeats = reservedSeats,
                ReservationEndDate = DateTime.Now.AddMinutes(30)
            };

            _mockSeatReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(seatReservation);
            _mockSeatReservationRepository.Setup(repo => repo.AddBookingConfirmationAsync(It.IsAny<BookingConfirmation>()))
                .ReturnsAsync((BookingConfirmation)null);

            var command = new AddBookingConfirmationCommand { ReservationId = reservationId };

            // Act & Assert
            await Assert.ThrowsAsync<BookingConfirmationException>(() => _handler.Handle(command, default));
        }
    }
}
