using BookingSystem.Abstractions.Repositories;
using BookingSystem.Domain.Models.Theater;
using BookingSystem.Domain.Models.Theater.Queries;
using BookingSystem.Domain.Services.TheaterManagement.QueryHandlers;
using Moq;

namespace UnitTests.BookingSystem.Domain.Services.TheaterManagement.QueryHandlers
{
    public class GetTheaterListQueryHandlerTests
    {
        private readonly Mock<ITheaterRepository> _mockTheaterRepository;
        private readonly GetTheaterListQueryHandler _handler;

        public GetTheaterListQueryHandlerTests()
        {
            _mockTheaterRepository = new Mock<ITheaterRepository>();
            _handler = new GetTheaterListQueryHandler(_mockTheaterRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTheaterList()
        {
            // Arrange
            var theaters = new List<Theater>
            {
                new Theater("Theater 1", 200) { Id = 1 },
                new Theater("Theater 2", 150) { Id = 2 }
            };
            _mockTheaterRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(theaters);
            var query = new GetTheaterListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(theaters[0], result);
            Assert.Contains(theaters[1], result);
        }


        [Fact]
        public async Task Handle_ReturnsEmptyTheaterList_WhenNoTheatersExist()
        {
            // Arrange
            _mockTheaterRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Theater>());

            // Act
            var result = await _handler.Handle(new GetTheaterListQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
