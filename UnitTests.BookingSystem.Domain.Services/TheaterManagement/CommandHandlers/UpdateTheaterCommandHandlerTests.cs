using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater.Exceptions;
using BookingSystem.Domain.Services.TheaterManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.Theater;

namespace UnitTests.BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class UpdateTheaterCommandHandlerTests
    {
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly Mock<ILogger<UpdateTheaterCommandHandler>> _mockLogger;
        private readonly UpdateTheaterCommandHandler _handler;

        public UpdateTheaterCommandHandlerTests()
        {
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _mockLogger = new Mock<ILogger<UpdateTheaterCommandHandler>>();
            _handler = new UpdateTheaterCommandHandler(_mockTheaterRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ValidTheater_ReturnsUpdatedTheater()
        {
            // Arrange
            var command = new UpdateTheaterCommand { Id = 1, Name = "Updated Theater", TotalSeats = 150 };
            var theater = new Theater(command.Name, command.TotalSeats) { Id = command.Id };
            _mockTheaterRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<Theater>()))
                .ReturnsAsync(theater);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.TotalSeats, result.TotalSeats);
        }

        [Fact]
        public async Task Handle_TheaterDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateTheaterCommand { Id = 99, Name = "Non-Existent Theater", TotalSeats = 100 };
            _mockTheaterRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<Theater>()))
                .ReturnsAsync((Theater)null);

            // Act & Assert
            await Assert.ThrowsAsync<TheaterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
