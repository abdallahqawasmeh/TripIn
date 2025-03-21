using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IFlightService
	{
		List<Flight> GetAllFlights();
		Flight? GetFlightById(int flightId);
		void CreateFlight(CreateFlightRequest flight);
		bool UpdateFlight(int FlightID,UpdateFlightRequest flight);
		void DeleteFlight(int flightId);
		List<Flight> GetFlightsByDate(SearchFlightsRequest request);


    }
}
