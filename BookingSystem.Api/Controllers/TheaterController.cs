using AutoMapper;
using BookingSystem.Api.ViewModels.Theater.Requests;
using BookingSystem.Api.ViewModels.Theater.Responses;
using BookingSystem.Domain.Models.Theater.Commands;
using BookingSystem.Domain.Models.Theater.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheaterController : ControllerBase
    {
        private readonly ILogger<TheaterController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TheaterController(IMediator mediator, IMapper mapper, ILogger<TheaterController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get Theaters list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Theater/allTheaters
        ///
        /// This endpoint will return a list of Theaters.
        /// </remarks>
        /// <response code="200">Returns Theaters list</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("allTheaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTheaters()
        {
            var theaters = await _mediator.Send(new GetTheaterListQuery());

            var theaterViewModels = _mapper.Map<IEnumerable<TheaterViewModel>>(theaters);

            return Ok(theaterViewModels);
        }

        /// <summary>
        /// Adds a new Theater.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Theater
        ///     {
        ///        "name": "Theater 1",
        ///        "totalSeats": 10
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Theater</response>
        /// <response code="400">If bad request or validation fails</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(TheaterViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTheater([FromBody] AddTheaterRequest addTheaterRequest)
        {
            _logger.LogInformation("New Theater will be added.");

            var addedTheater = await _mediator.Send(new AddTheaterCommand(addTheaterRequest.Name, addTheaterRequest.TotalSeats));

            return CreatedAtAction(nameof(GetById), new { id = addedTheater.Id }, addedTheater);
        }

        /// <summary>
        /// Updates details of an existing Theater.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Theater
        ///     {
        ///        "name": "Theater 1",
        ///        "totalSeats": 20
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated Theater.</response>
        /// <response code="400">If Theater is null or ID doesn't match.</response>
        /// <response code="404">If v cannot be found.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTheater(int id, [FromBody] UpdateTheaterRequest updateTheaterRequest)
        {
            _logger.LogInformation($"Theater ID: {id} will be updated.");

            var updatedMovie = await _mediator.Send(new UpdateTheaterCommand(id, updateTheaterRequest.Name, updateTheaterRequest.TotalSeats));

            return Ok(updatedMovie);
        }


        /// <summary>
        /// Get Theater by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Theater/{id}
        ///
        /// </remarks>
        /// <param name="id">ID of the Theater to retrieve.</param>
        /// <response code="200">Returns the Theater by ID</response>
        /// <response code="404">If no Theater is found by ID</response>

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var theater = await _mediator.Send(new GetTheaterByIdQuery(id));

            if (theater == null)
            {
                return NotFound();
            }

            var theaterViewModel = _mapper.Map<TheaterViewModel>(theater);
            return Ok(theaterViewModel);
        }

        /// <summary>
        /// Delete Theater by ID.
        /// </summary>
        /// <param name="id">Theater ID to delete.</param>
        /// <response code="204">Returns deleted Theater.</response>
        /// <response code="404">If Not Found Theater with ID.</response>
        /// <response code="400">If Theater could not be deleted.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTheater(int id)
        {
            _logger.LogInformation($"Theater ID: {id} will be deleted.");

            var deletedTheater = await _mediator.Send(new DeleteTheaterCommand(id));

            return Ok(deletedTheater);
        }
    }
}
