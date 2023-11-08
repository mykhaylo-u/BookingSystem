using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Services.ShowTimeManagement.QueryHandlers;
using Moq;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Queries;

namespace UnitTests.BookingSystem.Domain.Services.ShowTimeManagement.QueryHandler
{
    public class GetShowTimeByIdQueryHandlerTests
    {
        private readonly Mock<IShowTimeRepository> _mockShowTimeRepository;
        private readonly GetShowTimeByIdQueryHandler _handler;

        public GetShowTimeByIdQueryHandlerTests()
        {
            _mockShowTimeRepository = new Mock<IShowTimeRepository>();
            _handler = new GetShowTimeByIdQueryHandler(_mockShowTimeRepository.Object);
        }

        [Fact]
        public async Task Handle_ShowTimeExists_ReturnsShowTime()
        {
            // Arrange
            var showTimeId = 1;
            var showTime = new ShowTime { Id = showTimeId };
            var query = new GetShowTimeByIdQuery { Id = showTimeId };

            _mockShowTimeRepository.Setup(repo => repo.GetByIdAsync(showTimeId)).ReturnsAsync(showTime);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(showTimeId, result.Id);
        }

        [Fact]
        public async Task Handle_ShowTimeDoesNotExist_ReturnsNull()
        {
            // Arrange
            var showTimeId = 500;
            var query = new GetShowTimeByIdQuery { Id = showTimeId };

            _mockShowTimeRepository.Setup(repo => repo.GetByIdAsync(showTimeId)).ReturnsAsync((ShowTime)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
