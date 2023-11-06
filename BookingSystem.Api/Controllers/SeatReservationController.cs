using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookingSystem.Api.ViewModels.SeatReservation.Request;
using BookingSystem.Api.ViewModels.SeatReservation.Responses;
using BookingSystem.Domain.Models.SeatReservation.Commands;
using BookingSystem.Domain.Models.SeatReservation.Queries;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeatReservationController : ControllerBase
    {
        private readonly ILogger<SeatReservationController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SeatReservationController(ILogger<SeatReservationController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get SeatReservations list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /SeatReservation/allSeatReservations
        ///
        /// This endpoint will return a list of SeatReservations.
        /// </remarks>
        /// <response code="200">Returns movies list</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("AllSeatReservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSeatReservations()
        {
            var seatReservations = await _mediator.Send(new GetSeatReservationListQuery());
            var seatReservationViewModels = _mapper.Map<IEnumerable<SeatReservationViewModel>>(seatReservations);
            return Ok(seatReservationViewModels);
        }


        /// <summary>
        /// Get AvailableSeats list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /SeatReservation/AvailableSeats
        ///
        /// This endpoint will return a list of Available Seats.
        /// </remarks>
        /// <response code="200">Returns Available Seats list</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("AvailableSeats/{showTimeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableSeats(int showTimeId)
        {
            var seatReservations = await _mediator.Send(new GetAvailableSeatsQuery(showTimeId));
            var seatReservationViewModels = _mapper.Map<IEnumerable<SeatViewModel>>(seatReservations);
            return Ok(seatReservationViewModels);
        }

        /// <summary>
        /// Get SeatReservation by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /SeatReservation/{id}
        ///
        /// </remarks>
        /// <param name="id">ID of the SeatReservation to retrieve.</param>
        /// <response code="200">Returns the SeatReservation by ID</response>
        /// <response code="404">If no SeatReservation is found by ID</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var seatReservation = await _mediator.Send(new GetSeatReservationByIdQuery(id));

            if (seatReservation == null)
            {
                return NotFound();
            }

            var seatReservationViewModel = _mapper.Map<SeatReservationViewModel>(seatReservation);
            return Ok(seatReservationViewModel);
        }

        /// <summary>
        /// Adds a new SeatReservation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /SeatReservation
        ///     {
        ///        "ShowtimeId": "1",
        ///        "UserId": 1,
        ///        "ReservedSeatIds": [1,2]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created SeatReservation</response>
        /// <response code="400">If bad request or validation fails</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(SeatReservationViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSeatReservation([FromBody] AddSeatReservationRequest seatReservation)
        {
            var addedSeatReservation = await _mediator.Send(new AddSeatReservationCommand(seatReservation.ShowtimeId,
                seatReservation.UserId, seatReservation.ReservedSeatIds
            ));

            return CreatedAtAction(nameof(GetById), new { id = addedSeatReservation.Id }, addedSeatReservation);
        }

        /// <summary>
        /// Adds a new ConfirmBooking.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST SeatReservation/ConfirmBooking
        ///     {
        ///        "ShowtimeId": "1",
        ///        "UserId": 1,
        ///        "ReservedSeatIds": [1,2]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created SeatReservation</response>
        /// <response code="400">If bad request or validation fails</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("ConfirmBooking")]
        [ProducesResponseType(typeof(SeatReservationViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmBooking(int reservationId)
        {
            var addedConfirmedBooking = await _mediator.Send(new AddBookingConfirmationCommand(reservationId));

            return CreatedAtAction(nameof(GetById), new { id = addedConfirmedBooking.Id }, addedConfirmedBooking);
        }


        ///// <summary>
        ///// Updates the details of an existing SeatReservation.
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     PUT /api/SeatReservations/update
        /////     {
        /////        "title": "HomeVideo",
        /////        "duration": 180,
        /////        "genre": "Comedy",
        /////        "summary": "Some super long summary",
        /////        "showStartDate": "2023-11-10",
        /////        "showEndDate": "2023-11-24"
        /////     }
        /////
        ///// </remarks>
        ///// <param name="id">The ID of the SeatReservation to update.</param>
        ///// <param name="seatReservation">The SeatReservation information to update.</param>
        ///// <response code="200">Returns the updated SeatReservation.</response>
        ///// <response code="400">If the SeatReservation is null or the ID doesn't match.</response>
        ///// <response code="404">If the SeatReservation cannot be found.</response>
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdateSeatReservation(int id, [FromBody] CreateNewSeatReservationRequest seatReservation)
        //{
        //    var updatedSeatReservation = await _mediator.Send(new UpdateSeatReservationCommand(id, seatReservation.Title,
        //        TimeSpan.FromMinutes(seatReservation.Duration), seatReservation.Genre, seatReservation.Summary,
        //        DateTime.Parse(seatReservation.ShowStartDate),
        //        DateTime.Parse(seatReservation.ShowEndDate)
        //    ));

        //    return Ok(updatedSeatReservation);
        //}

        ///// <summary>
        ///// Delete SeatReservation by ID.
        ///// </summary>
        ///// <param name="id">The ID of the SeatReservation to delete.</param>
        ///// <response code="204">Returns deleted SeatReservation.</response>
        ///// <response code="404">If Not Found the SeatReservation with ID.</response>
        ///// <response code="400">If the SeatReservation could not be deleted.</response>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> DeleteSeatReservation(int id)
        //{
        //    var deletedSeatReservation = await _mediator.Send(new DeleteSeatReservationCommand(id));

        //    return Ok(deletedSeatReservation);

        //    //return NoContent();
        //}
    }
}