using FlightTracker.API.Requests.Airport;
using FlightTracker.Core.Data;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AirportController : ControllerBase
	{


		private readonly IAirportService _airportService;

		public AirportController(IAirportService airportService)
		{
			_airportService = airportService;
		}

		
		[HttpPost]
		public IActionResult CreateAirport([FromBody] CreateAirportRequest airport)
		{
			if (airport == null)
				return BadRequest("Airport data is invalid.");
			
			_airportService.CreateAirport(airport);
			return NoContent();
		}

		[HttpGet("{airportId}")]
		public IActionResult GetAirportById(int airportId)
		{
			var airport = _airportService.GetAirportById(airportId);
			if (airport == null)
				return NotFound();

			return Ok(airport);
		}

		[HttpGet]
		public IActionResult GetAllAirports()
		{
			var airports = _airportService.GetAllAirports();
			return Ok(airports);
		}

		[HttpPut("{airportId}")]
		public IActionResult UpdateAirport(int airportId, [FromBody] UpdateAirportRequest airport)
		{
			if (airport == null )
				return BadRequest("Invalid airport data.");
			var isFound = _airportService.UpdateAirport(airportId, airport);

            if (!isFound)
				return NotFound();
			

			return NoContent();
		}


		[HttpDelete("{airportId}")]
		public IActionResult DeleteAirport(int airportId)
		{
			var existingAirport = _airportService.GetAirportById(airportId);
			if (existingAirport == null)
				return NotFound();

			_airportService.DeleteAirport(airportId);
			return NoContent();
		}

	}
}
