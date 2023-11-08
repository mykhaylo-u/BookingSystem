using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Services.TheaterManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Exceptions;

namespace UnitTests.BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class DeleteTheaterCommandHandlerTests
    {
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly Mock<ILogger<DeleteTheaterCommandHandler>> _mockLogger;
        private readonly DeleteTheaterCommandHandler _handler;

        public DeleteTheaterCommandHandlerTests()
        {
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _mockLogger = new Mock<ILogger<DeleteTheaterCommandHandler>>();
            _handler = new DeleteTheaterCommandHandler(_mockTheaterRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ValidId_DeletesTheater()
        {
            // Arrange
            var command = new DeleteTheaterCommand { Id = 1 };
            var theater = new Theater("Some Theater", 100) { Id = command.Id };
            _mockTheaterRepository.Setup(repo => repo.DeleteAsync(command.Id))
                .ReturnsAsync(theater);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            var command = new DeleteTheaterCommand { Id = 500 };
            _mockTheaterRepository.Setup(repo => repo.DeleteAsync(command.Id))
                .ReturnsAsync((Theater)null);

            // Act & Assert
            await Assert.ThrowsAsync<TheaterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
