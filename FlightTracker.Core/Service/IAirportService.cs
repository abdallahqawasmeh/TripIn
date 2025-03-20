using FlightTracker.API.Requests.Airport;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IAirportService
	{


		
		void CreateAirport(CreateAirportRequest airport);
		Airport? GetAirportById(int airportId);
		List<Airport> GetAllAirports();
		bool UpdateAirport(int AirportId, UpdateAirportRequest airport);
		void DeleteAirport(int airportId);

	}
}
