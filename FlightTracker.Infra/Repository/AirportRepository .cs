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
	public class AirportRepository : IAirportRepository
	{
		private readonly IDbContext _dbContext;

		public AirportRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateAirport(Airport airport)
		{
			var p = new DynamicParameters();
			p.Add("p_Name", airport.Name, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_City", airport.City, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Country", airport.Country, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Status", airport.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_Longitude", airport.Longitude, dbType: DbType.Double, direction: ParameterDirection.Input);
			p.Add("p_Latitude", airport.Latitude, dbType: DbType.Double, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AIRPORTS.CreateAirport", p, commandType: CommandType.StoredProcedure);
		}

		public Airport? GetAirportById(int airportId)
		{
			var p = new DynamicParameters();
			p.Add("p_AirportId", airportId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Airport> result = _dbContext.Connection.Query<Airport>("PKG_AIRPORTS.GetAirportById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public List<Airport> GetAllAirports()
		{
			IEnumerable<Airport> result = _dbContext.Connection.Query<Airport>("PKG_AIRPORTS.GetAllAirports", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public void UpdateAirport(Airport airport)
		{
			var p = new DynamicParameters();
			p.Add("p_AirportId", airport.Airportid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_Name", airport.Name, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_City", airport.City, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Country", airport.Country, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Status", airport.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_Longitude", airport.Longitude, dbType: DbType.Double, direction: ParameterDirection.Input);
			p.Add("p_Latitude", airport.Latitude, dbType: DbType.Double, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AIRPORTS.UpdateAirport", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteAirport(int airportId)
		{
			var p = new DynamicParameters();
			p.Add("p_AirportId", airportId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AIRPORTS.DeleteAirport", p, commandType: CommandType.StoredProcedure);
		}
	}
}
