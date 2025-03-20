using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.Flight;
using FlightTracker.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Service
{
	public class FlightService:IFlightService
	{



		private readonly IFlightRepository _flightRepository;

		public FlightService(IFlightRepository flightRepository)
		{
			_flightRepository = flightRepository;
		}

		public void CreateFlight(CreateFlightRequest flight)
		{
			var newFlight = new Flight()
			{
				Arrivalairportid = flight.Arrivalairportid,
				Departureairportid = flight.Departureairportid,
				Arrivaltime = flight.Arrivaltime,
				Companyid = flight.Companyid,
				Departuretime = flight.Departuretime,
				Flightnumber = flight.FlightName,
				Availableseats = flight.Availableseats,
				Status = 1,
				Price = flight.Price,
				Numberofpassengers = 0


			};


			_flightRepository.CreateFlight(newFlight);


		}

		public void DeleteFlight(int flightId)
		{

			_flightRepository.DeleteFlight(flightId);

		}

		public List<Flight> GetAllFlights()
		{
			return _flightRepository.GetAllFlights();
		}

		public Flight? GetFlightById(int flightId)
		{
			return _flightRepository.GetFlightById(flightId);
		}

		public bool UpdateFlight(int flightId,UpdateFlightRequest flight)
		{
			var oldFlight = _flightRepository.GetFlightById(flightId);
			if (oldFlight == null)
				return false;
			oldFlight.Status = flight.Status ?? oldFlight.Status;
			_flightRepository.UpdateFlight(oldFlight);
			return true;
		}
	}
}
