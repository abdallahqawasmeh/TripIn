using FlightTracker.Core.Requests.Flight;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightController : ControllerBase
	{





		private readonly IFlightService _flightService;

		public FlightController(IFlightService flightService)
		{
			_flightService = flightService;
		}

		[HttpPost]
		public IActionResult CreateFlight([FromBody] CreateFlightRequest flightRequest)
		{
			if (flightRequest == null)
				return BadRequest("Flight data is invalid.");

			 _flightService.CreateFlight(flightRequest);
			return  NoContent();
		}

		[HttpGet("{flightId}")]
		public IActionResult GetFlightById(int flightId)
		{
			var flight = _flightService.GetFlightById(flightId);
			if (flight == null)
				return NotFound();

			return Ok(flight);
		}

		[HttpGet]
		public IActionResult GetAllFlights()
		{
			var flights = _flightService.GetAllFlights();
			return Ok(flights);
		}

		[HttpPut("{flightId}")]
		public IActionResult UpdateFlight(int flightId, [FromBody] UpdateFlightRequest flightRequest)
		{
			if (flightRequest == null)
				return BadRequest("Invalid flight data.");

			var result = _flightService.UpdateFlight(flightId, flightRequest);
			if (!result)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{flightId}")]
		public IActionResult DeleteFlight(int flightId)
		{
			var flight = _flightService.GetFlightById(flightId);
			if (flight == null)
				return NotFound();

			_flightService.DeleteFlight(flightId);
			return NoContent();
		}























	}
}
