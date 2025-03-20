using Dapper;
using FlightTracker.Core.Common;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Repository
{
	public class FlightRepository : IFlightRepository
	{
		private readonly IDbContext _dbContext;

		public FlightRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Flight> GetAllFlights()
		{
			IEnumerable<Flight> result = _dbContext.Connection.Query<Flight>("Flights_Package.GetAllFlights", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public Flight? GetFlightById(int flightId)
		{
			var p = new DynamicParameters();
			p.Add("flight_id", flightId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Flight> result = _dbContext.Connection.Query<Flight>("Flights_Package.GetFlightById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public void CreateFlight(Flight flight)
		{
			var p = new DynamicParameters();
			p.Add("flight_name", flight.Flightnumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("departure_time", flight.Departuretime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("arrival_time", flight.Arrivaltime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("price", flight.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("available_seats", flight.Availableseats, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("status", flight.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("arrival_airport", flight.Arrivalairportid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("departure_airport", flight.Departureairportid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("company_id", flight.Companyid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("number_of_passengers", flight.Numberofpassengers, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("Flights_Package.CreateFlight", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateFlight(Flight flight)
		{
			var p = new DynamicParameters();
			p.Add("flight_id", flight.Flightid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("flight_name", flight.Flightnumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("departure_time", flight.Departuretime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("arrival_time", flight.Arrivaltime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("price", flight.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("available_seats", flight.Availableseats, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("status", flight.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("arrival_airport", flight.Arrivalairportid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("departure_airport", flight.Departureairportid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("company_id", flight.Companyid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("number_of_passengers", flight.Numberofpassengers, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("Flights_Package.UpdateFlight", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteFlight(int flightId)
		{
			var p = new DynamicParameters();
			p.Add("flight_id", flightId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("Flights_Package.DeleteFlight", p, commandType: CommandType.StoredProcedure);
		}
	}
}
