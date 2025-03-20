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
	public class BookFlightRepository :IBookFlightRepository
	{
		private readonly IDbContext _dbContext;

		public BookFlightRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateBooking(Bookflight booking)
		{
			var p = new DynamicParameters();
			p.Add("number_of_passengers", booking.Allnumberofpassengers, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("flight_id", booking.Flightid, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("BookFlight_Package.CreateBooking", p, commandType: CommandType.StoredProcedure);
		}

		public Bookflight? GetBookingById(int bookingId)
		{
			var p = new DynamicParameters();
			p.Add("booking_id", bookingId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Bookflight> result = _dbContext.Connection.Query<Bookflight>("BookFlight_Package.GetBookingById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}
	}
}
