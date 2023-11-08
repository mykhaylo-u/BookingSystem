using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater.Exceptions;
using BookingSystem.Domain.Services.TheaterManagement.CommandHandlers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Models.Theater;

namespace UnitTests.BookingSystem.Domain.Services.TheaterManagement.CommandHandlers
{
    public class AddTheaterCommandHandlerTests
    {
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly Mock<ILogger<AddTheaterCommandHandler>> _mockLogger;
        private readonly AddTheaterCommandHandler _handler;

        public AddTheaterCommandHandlerTests()
        {
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _mockLogger = new Mock<ILogger<AddTheaterCommandHandler>>();
            _handler = new AddTheaterCommandHandler(_mockTheaterRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_InvokesAddAsyncOnRepository()
        {
            // Arrange
            var command = new AddTheaterCommand { Name = "New Theater", TotalSeats = 100 };
            var theater = new Theater(command.Name, command.TotalSeats);
            _mockTheaterRepository.Setup(repo => repo.AddAsync(It.IsAny<Theater>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(theater);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.TotalSeats, result.TotalSeats);
            _mockTheaterRepository.Verify(repo => repo.AddAsync(It.IsAny<Theater>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_AddAsyncReturnsNull_ThrowsTheaterCreationException()
        {
            // Arrange
            var command = new AddTheaterCommand { Name = "New Theater", TotalSeats = 100 };
            _mockTheaterRepository.Setup(repo => repo.AddAsync(It.IsAny<Theater>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Theater)null);

            // Act & Assert
            await Assert.ThrowsAsync<TheaterCreationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
