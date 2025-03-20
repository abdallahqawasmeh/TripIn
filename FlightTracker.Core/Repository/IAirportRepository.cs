using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{

	public interface IAirportRepository
	{
		void CreateAirport(Airport airport);
		Airport? GetAirportById(int airportId);
		List<Airport> GetAllAirports();
		void UpdateAirport(Airport airport);
		void DeleteAirport(int airportId);
	}
}
