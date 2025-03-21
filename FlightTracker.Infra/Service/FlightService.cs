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
	public class FlightService : IFlightService
	{


		private readonly IAirportRepository _airportRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly ICompanyRepository _companyRepository;

		public FlightService(IAirportRepository airportRepository, IFlightRepository flightRepository, ICompanyRepository companyRepository)
		{
			_airportRepository = airportRepository;
			_flightRepository = flightRepository;
			_companyRepository = companyRepository;
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
			var flight = _flightRepository.GetFlightById(flightId);
			if (flight == null)
				return null;
            var arrAir = _airportRepository.GetAirportById((int)flight.Arrivalairportid!); 
			var depAir = _airportRepository.GetAirportById((int)flight.Departureairportid!);
			var comp = _companyRepository.GetCompanyById((int)flight.Companyid)!;
			flight.Arrivalairport = arrAir;
            flight.Departureairport = depAir;
			flight.Company = comp;
			return flight;

        }

        public bool UpdateFlight(int flightId, UpdateFlightRequest flight)
		{
			var oldFlight = _flightRepository.GetFlightById(flightId);
			if (oldFlight == null)
				return false;
			oldFlight.Status = flight.Status ?? oldFlight.Status;
			_flightRepository.UpdateFlight(oldFlight);
			return true;
		}
		public List<Flight> GetFlightsByDate(SearchFlightsRequest request)
		{
			var isRangeNull = request.EndDateOnly == null || request.StartDateOnly == null || request.StartDateOnly == null;


			var flights = GetAllFlights().Where(
				x =>
			(isRangeNull || (DateOnly.FromDateTime(x.Departuretime) >= request.StartDateOnly && DateOnly.FromDateTime(x.Departuretime) <= request.EndDateOnly))
			&& (request.ArrivalAirPortId == x.Arrivalairportid && x.Departureairportid == request.DepartureAirportId)
			);
			if (request.Des)
			{
				if (request.SortByFastest)
					return flights.OrderByDescending(x => x.Duration).ToList();
				return flights.OrderByDescending(x => x.Price).ToList();
			}

			if (request.SortByFastest)
				return flights.OrderBy(x => x.Duration).ToList();
			return flights.OrderBy(x => x.Price).ToList();
		}
	}
}