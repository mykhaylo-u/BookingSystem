using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater.Queries;
using BookingSystem.Domain.Services.TheaterManagement.QueryHandlers;
using Moq;
using BookingSystem.Domain.Models.Theater;

namespace UnitTests.BookingSystem.Domain.Services.TheaterManagement.QueryHandlers
{
    public class GetTheaterByIdQueryHandlerTests
    {
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly GetTheaterByIdQueryHandler _handler;

        public GetTheaterByIdQueryHandlerTests()
        {
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _handler = new GetTheaterByIdQueryHandler(_mockTheaterRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsTheater()
        {
            // Arrange
            var theaterId = 1;
            var theater = new Theater("Theater 1", 200) { Id = theaterId };
            _mockTheaterRepository.Setup(repo => repo.GetByIdAsync(theaterId)).ReturnsAsync(theater);
            var query = new GetTheaterByIdQuery { Id = theaterId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(theaterId, result.Id);
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsNull()
        {
            // Arrange
            var theaterId = 99;
            _mockTheaterRepository.Setup(repo => repo.GetByIdAsync(theaterId)).ReturnsAsync((Theater)null);
            var query = new GetTheaterByIdQuery { Id = theaterId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }

}
