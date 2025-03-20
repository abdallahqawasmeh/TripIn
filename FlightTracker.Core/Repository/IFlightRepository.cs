using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IFlightRepository
	{
		List<Flight> GetAllFlights();
		Flight? GetFlightById(int flightId);
		void CreateFlight(Flight flight);
		void UpdateFlight(Flight flight);
		void DeleteFlight(int flightId);
	}
}
