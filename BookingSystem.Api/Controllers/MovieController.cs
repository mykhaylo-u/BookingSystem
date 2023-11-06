using BookingSystem.Domain.Models.Movie.Commands;
using BookingSystem.Domain.Models.Movie;
using BookingSystem.Domain.Models.Movie.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookingSystem.Api.ViewModels.Movie.Requests;
using BookingSystem.Api.ViewModels.Movie.Responses;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieController(ILogger<MovieController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Movies list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Movie/allMovies
        ///
        /// This endpoint will return a list of movies.
        /// </remarks>
        /// <response code="200">Returns movies list</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("allMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _mediator.Send(new GetMovieListQuery());
            var movieViewModels = _mapper.Map<IEnumerable<MovieViewModel>>(movies);
            return Ok(movieViewModels);
        }

        /// <summary>
        /// Get Movie by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Movie/{id}
        ///
        /// </remarks>
        /// <param name="id">ID of the movie to retrieve.</param>
        /// <response code="200">Returns the movie by ID</response>
        /// <response code="404">If no movie is found by ID</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _mediator.Send(new GetMovieByIdQuery(id));

            if (movie == null)
            {
                return NotFound();
            }

            var movieViewModel = _mapper.Map<MovieViewModel>(movie);
            return Ok(movieViewModel);
        }

        /// <summary>
        /// Adds a new Movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movie
        ///     {
        ///        "title": "SomeVideo",
        ///        "duration": 180,
        ///        "genre": "Comedy",
        ///        "summary": "Some super long summary",
        ///        "showStartDate": "2023-11-25",
        ///        "showEndDate": "2023-11-29"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created movie</response>
        /// <response code="400">If bad request or validation fails</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest movie)
        {
            var addedMovie = await _mediator.Send(new AddNewMovieCommand(movie.Title,
                TimeSpan.FromMinutes(movie.Duration), movie.Genre, movie.Summary,
                DateTime.Parse(movie.ShowStartDate),
                DateTime.Parse(movie.ShowEndDate)
            ));

            return CreatedAtAction(nameof(GetById), new { id = addedMovie.Id }, addedMovie);
        }


        /// <summary>
        /// Updates details of an existing Movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Movie/{id}
        ///     {
        ///        "title": "SomeVideoUpdated",
        ///        "duration": 160,
        ///        "genre": "Comedy",
        ///        "summary": "Some super long summary updated",
        ///        "showStartDate": "2023-11-25",
        ///        "showEndDate": "2023-11-29"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Movie ID to update.</param>
        /// <param name="movie">Movie information to update.</param>
        /// <response code="200">Returns updated movie.</response>
        /// <response code="400">If movie is null or ID doesn't match.</response>
        /// <response code="404">If movie cannot be found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieRequest movie)
        {
            var updatedMovie = await _mediator.Send(new UpdateMovieCommand(id, movie.Title,
                TimeSpan.FromMinutes(movie.Duration), movie.Genre, movie.Summary,
                DateTime.Parse(movie.ShowStartDate),
                DateTime.Parse(movie.ShowEndDate)
            ));

            return Ok(updatedMovie);
        }

        /// <summary>
        /// Delete Movie by ID.
        /// </summary>
        /// <param name="id">Movie ID to delete.</param>
        /// <response code="204">Returns deleted movie.</response>
        /// <response code="404">If Not Found movie with ID.</response>
        /// <response code="400">If movie could not be deleted.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deletedMovie = await _mediator.Send(new DeleteMovieCommand(id));

            return Ok(deletedMovie);

            //return NoContent();
        }
    }
}