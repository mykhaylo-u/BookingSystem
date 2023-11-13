using AutoMapper;
using BookingSystem.Api.ViewModels.ShowTime;
using BookingSystem.Api.ViewModels.ShowTime.Request;
using BookingSystem.Domain.Models.Showtime.Commands;
using BookingSystem.Domain.Models.Showtime.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowTimeController : ControllerBase
    {
        private readonly ILogger<ShowTimeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShowTimeController(ILogger<ShowTimeController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get ShowTime list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ShowTime/allShowTimes
        ///
        /// This endpoint will return a list of showTimes.
        /// </remarks>
        /// <response code="200">Returns ShowTime list</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("allShowTimes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllShowTimes()
        {
            var showTimes = await _mediator.Send(new GetShowTimesListQuery());

            var showTimeViewModels = _mapper.Map<IEnumerable<ShowTimeViewModel>>(showTimes);

            return Ok(showTimeViewModels);
        }

        /// <summary>
        /// Get ShowTime by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ShowTime/{id}
        ///
        /// </remarks>
        /// <param name="id">ID of the ShowTime to retrieve.</param>
        /// <response code="200">Returns the ShowTime by ID</response>
        /// <response code="404">If no ShowTime is found by ID</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var showTime = await _mediator.Send(new GetShowTimeByIdQuery(id));

            if (showTime == null)
            {
                return NotFound();
            }

            var showTimeViewModel = _mapper.Map<ShowTimeViewModel>(showTime);

            return Ok(showTimeViewModel);
        }

        /// <summary>
        /// Adds a new ShowTime.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /ShowTime
        ///     {
        ///        "movieId": 1,
        ///        "theaterId": 2,
        ///        "startDateTime": "2023-11-25",
        ///        "endDateTime": "2023-11-29",
        ///        "ticketPrice": 120,
        ///        "seats": [
        ///             { "row": 1, "number": 1, "isAvailable": true },
        ///             { "row": 1, "number": 2, "isAvailable": true }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created ShowTime</response>
        /// <response code="400">If bad request or validation fails</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(ShowTimeViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddShowTime([FromBody] AddShowTimeRequest addShowTimeRequest)
        {
            _logger.LogInformation("New ShowTime will be added.");

            var addedShowTime = await _mediator.Send(new AddShowTimeCommand(addShowTimeRequest.MovieId,
                addShowTimeRequest.TheaterId,
                addShowTimeRequest.StartDateTime,
                addShowTimeRequest.EndDateTime, addShowTimeRequest.TicketPrice, addShowTimeRequest.Seats
            ));

            return CreatedAtAction(nameof(GetById), new { id = addedShowTime.Id }, addedShowTime);
        }


        /// <summary>
        /// Updates details of an existing ShowTime.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /ShowTime/{id}
        ///     {
        ///        "movieId": 1,
        ///        "theaterId": 2,
        ///        "startDateTime": "2023-11-25",
        ///        "endDateTime": "2023-11-29",
        ///        "ticketPrice": 10,
        ///        "seats": [
        ///             { "row": 1, "number": 1, "isAvailable": true },
        ///             { "row": 1, "number": 2, "isAvailable": true }
        ///             { "row": 1, "number": 3, "isAvailable": true }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ShowTime ID to update.</param>
        /// <param name="updateShowTimeRequest">ShowTime information to update.</param>
        /// <response code="200">Returns updated ShowTime.</response>
        /// <response code="400">If ShowTime is null or ID doesn't match.</response>
        /// <response code="404">If ShowTime cannot be found.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateShowTime(int id, [FromBody] UpdateShowTimeRequest updateShowTimeRequest)
        {
            _logger.LogInformation($"ShowTime ID: {id} will be updated.");

            var updatedShowTime = await _mediator.Send(new UpdateShowTimeCommand(id, updateShowTimeRequest.MovieId,
                updateShowTimeRequest.TheaterId, updateShowTimeRequest.StartDateTime,
                updateShowTimeRequest.EndDateTime, updateShowTimeRequest.TicketPrice, updateShowTimeRequest.Seats
            ));

            return Ok(updatedShowTime);
        }

        /// <summary>
        /// Delete ShowTime by ID.
        /// </summary>
        /// <param name="id">ShowTime ID to delete.</param>
        /// <response code="204">Returns deleted ShowTime.</response>
        /// <response code="404">If Not Found ShowTime with ID.</response>
        /// <response code="400">If ShowTime could not be deleted.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            _logger.LogInformation($"ShowTime ID: {id} will be deleted.");

            var deletedShowTime = await _mediator.Send(new DeleteShowTimeCommand(id));

            return Ok(deletedShowTime);
        }
    }
}
