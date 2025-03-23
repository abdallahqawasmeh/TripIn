using FlightTracker.Core.Requests.Flight;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
		
		[HttpPatch ("byDate")]
		public IActionResult GetFlightsByDate([FromBody]SearchFlightsRequest request)
		{
            if (request.End == null && request.Start != null)
                return BadRequest("the end and the start should be realatable "); 
			if (request.End != null && request.Start == null)
                return BadRequest("the end and the start should be realatable ");

            return Ok(_flightService.GetFlightsByDate(request));
		}


		[HttpPost("book/{flightId}")]
		[Authorize(Roles ="user")]
		
		public async Task<IActionResult> BookFlight(int flightId,int numberOfPassengers)
		{
			var result = await _flightService.BookFlight(flightId, numberOfPassengers);
			if (result == null)
				return NotFound();
			if(result== false )
				return BadRequest("not enough money poor man");
			return Ok(new {message="succefull payment!"});


		}


		[HttpGet("invoice")]
		public async Task<IActionResult> DownloadInvoice(string filePath)
		{
			var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

			if (!System.IO.File.Exists(absolutePath))
				return NotFound("Invoice file not found");

			return PhysicalFile(absolutePath, "application/pdf", Path.GetFileName(absolutePath));
		}

		[HttpGet("statistics/{flightId}")]
		[Authorize(Roles ="admin")]
		public IActionResult GetStatistics (int flightId)
		{
			var result = _flightService.GetFlightStatistics(flightId);
			if (result == null)
				return NotFound();
			return Ok(result);
		}


















	}
}
