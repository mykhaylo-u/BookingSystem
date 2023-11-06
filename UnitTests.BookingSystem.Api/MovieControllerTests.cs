using System.Diagnostics;
using AutoMapper;
using BookingSystem.Api.Controllers;
using BookingSystem.Api.ViewModels.Movie.Requests;
using BookingSystem.Api.ViewModels.Movie.Responses;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie.Queries;
using BookingSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.BookingSystem.Api
{
    public class MovieControllerTests
    {
        private readonly Mock<ILogger<MovieController>> _mockLogger = new();
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly MovieController _controller;

        private readonly List<Movie> _movies = new()
        {
            new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today, DateTime.Today.AddDays(30), "summary1"),
            new Movie("2nd", TimeSpan.FromMinutes(222), "comedy", DateTime.Today.AddDays(2), DateTime.Today.AddDays(15), "summary2")
        };

        private readonly List<MovieViewModel> _movieViewModels = new()
        {
            new MovieViewModel(),
            new MovieViewModel()
        };

        public MovieControllerTests()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetMovieListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_movies);

            _mockMapper.Setup(m => m.Map<IEnumerable<MovieViewModel>>(_movies))
                .Returns(_movieViewModels);

            _controller = new MovieController(_mockLogger.Object, _mockMediator.Object, _mockMapper.Object);
        }

        #region GetAllMovies

        [Fact]
        public async Task GetAvailableMovies_Returns_OkResult_With_Movies()
        {
            // Act
            var result = await _controller.GetAllMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovies = Assert.IsAssignableFrom<IEnumerable<MovieViewModel>>(okResult.Value);
            Assert.Equal(_movieViewModels.Count, ((List<MovieViewModel>)returnedMovies).Count);
        }

        [Fact]
        public async Task GetAvailableMovies_Returns_EmptyList_When_NoMoviesAvailable()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetMovieListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Movie>());

            // Act
            var result = await _controller.GetAllMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovies = Assert.IsAssignableFrom<IEnumerable<MovieViewModel>>(okResult.Value);
            Assert.Empty(returnedMovies);
        }

        [Fact]
        public async Task GetAvailableMovies_Returns_InternalServerError_OnException()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetMovieListQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetAllMovies());
        }

        #endregion

        #region GetById

        [Fact]
        public async Task GetById_MovieExists_ReturnsOkResultWithMovie()
        {
            // Arrange
            var movieId = 1;
            var movie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today, DateTime.Today.AddDays(30), "summary1");
            var movieViewModel = new MovieViewModel();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(movie);
            _mockMapper.Setup(x => x.Map<MovieViewModel>(It.IsAny<Movie>()))
                      .Returns(movieViewModel);

            // Act
            var result = await _controller.GetById(movieId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MovieViewModel>(okResult.Value);
            Assert.Equal(movieViewModel, returnValue);
        }

        [Fact]
        public async Task GetById_MovieDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var movieId = 1;
            
            _mockMediator.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Movie)null!);

            // Act
            var result = await _controller.GetById(movieId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_MovieExists_MapperMapsToViewModel()
        {
            // Arrange
            var movieId = 1;
            var movie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today, DateTime.Today.AddDays(30), "summary1");
            
            _mockMediator.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(movie);

            // Act
            await _controller.GetById(movieId);

            // Assert
            _mockMapper.Verify(x => x.Map<MovieViewModel>(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task GetById_ThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var movieId = 1;
            
            _mockMediator.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Error retrieving data"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetById(movieId));
        }

        #endregion

        #region AddMovie

        [Fact]
        public async Task AddMovie_ValidRequest_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var movieRequest = new AddMovieRequest
            {
                Title = "1st",
                Duration = 120,
                Genre = "comedy",
                ShowStartDate = DateTime.Today.ToString(Constants.DefaultDateFormat),
                ShowEndDate = DateTime.Today.AddDays(30).ToString(Constants.DefaultDateFormat),
                Summary = "summary1"
            };

            var movie = new Movie("1st", TimeSpan.FromMinutes(120), "comedy", DateTime.Today, DateTime.Today.AddDays(30), "summary1")
            {
                Id = 1
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<AddNewMovieCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(movie);

            // Act
            var result = await _controller.AddMovie(movieRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
            Debug.Assert(createdAtActionResult.RouteValues != null, "createdAtActionResult.RouteValues != null");
            Assert.Equal(movie.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(movie, createdAtActionResult.Value);
        }

        [Fact]
        public async Task AddMovie_CreationFails_ThrowsException()
        {
            // Arrange
            var movieRequest = new AddMovieRequest
            {
                // Initialize properties with valid data
            };
            _mockMediator.Setup(x => x.Send(It.IsAny<AddNewMovieCommand>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Error creating movie"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.AddMovie(movieRequest));
        }

        #endregion
    }
}