using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Showtime;
using BookingSystem.Domain.Models.Showtime.Queries;
using BookingSystem.Domain.Services.ShowtimeManagement.QueryHandlers;
using Moq;

namespace UnitTests.BookingSystem.Domain.Services.ShowTimeManagement.QueryHandler
{
    public class GetShowTimesListQueryHandlerTests
    {
        private readonly Mock<IShowTimeRepository> _mockShowTimeRepository;
        private readonly GetShowTimesListQueryHandler _handler;

        public GetShowTimesListQueryHandlerTests()
        {
            _mockShowTimeRepository = new Mock<IShowTimeRepository>();
            _handler = new GetShowTimesListQueryHandler(_mockShowTimeRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsAllShowTimes()
        {
            // Arrange
            var showTimes = new List<ShowTime>
            {
                new ShowTime { Id = 1 },
                new ShowTime { Id = 2 },
            };

            _mockShowTimeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(showTimes);

            // Act
            var result = await _handler.Handle(new GetShowTimesListQuery(), CancellationToken.None);

            // Assert
            var resultList = result.ToList();
            Assert.Equal(showTimes.Count, resultList.Count);
            for (int i = 0; i < showTimes.Count; i++)
            {
                Assert.Equal(showTimes[i].Id, resultList[i].Id);
            }
        }

        [Fact]
        public async Task Handle_RepositoryReturnsEmptyList_ReturnsEmptyEnumerable()
        {
            // Arrange
            var showTimes = new List<ShowTime>();

            _mockShowTimeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(showTimes);

            // Act
            var result = await _handler.Handle(new GetShowTimesListQuery(), CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }
    }
}
