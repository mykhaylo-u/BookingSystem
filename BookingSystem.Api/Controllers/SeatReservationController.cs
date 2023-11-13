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
        [HttpGet("allSeatReservations")]
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
        [HttpGet("availableSeats/{showTimeId:int}")]
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
        public async Task<IActionResult> AddSeatReservation(
            [FromBody] AddSeatReservationRequest addSeatReservationRequest)
        {
            _logger.LogInformation("New reservation will be added.");

            var addedSeatReservation = await _mediator.Send(new AddSeatReservationCommand(
                addSeatReservationRequest.ShowtimeId,
                addSeatReservationRequest.UserId, addSeatReservationRequest.ReservedSeatIds
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
        [HttpPost("confirmBooking")]
        [ProducesResponseType(typeof(SeatReservationViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmBooking(int reservationId)
        {
            _logger.LogInformation($"Reservation {reservationId} will be confirmed.");

            var addedConfirmedBooking = await _mediator.Send(new AddBookingConfirmationCommand(reservationId));

            return CreatedAtAction(nameof(GetById), new { id = addedConfirmedBooking.Id }, addedConfirmedBooking);
        }
    }
}