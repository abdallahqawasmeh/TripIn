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
	public class ReservationRepository : IReservationRepository
	{
		private readonly IDbContext _dbContext;

		public ReservationRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Reservation> GetAllReservations()
		{
			IEnumerable<Reservation> result = _dbContext.Connection.Query<Reservation>("PKG_RESERVATION.GetAllReservations", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public Reservation? GetReservationById(int reservationId)
		{
			var p = new DynamicParameters();
			p.Add("p_ReservationId", reservationId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Reservation> result = _dbContext.Connection.Query<Reservation>("PKG_RESERVATION.GetReservationById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public List<Reservation> GetReservationsByUser(int userId)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Reservation> result = _dbContext.Connection.Query<Reservation>("PKG_RESERVATION.GetReservationsByUser", p, commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public List<Reservation> GetReservationsByFlight(int flightId)
		{
			var p = new DynamicParameters();
			p.Add("p_FlightId", flightId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Reservation> result = _dbContext.Connection.Query<Reservation>("PKG_RESERVATION.GetReservationsByFlight", p, commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public void CreateReservation(Reservation reservation)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", reservation.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FlightId", reservation.Flightid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_TotalAmount", reservation.Totalamount, dbType: DbType.Decimal, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_RESERVATION.CreateReservation", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateReservation(Reservation reservation)
		{
			var p = new DynamicParameters();
			p.Add("p_ReservationId", reservation.Reservationid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_UserId", reservation.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FlightId", reservation.Flightid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_TotalAmount", reservation.Totalamount, dbType: DbType.Decimal, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_RESERVATION.UpdateReservation", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteReservation(int reservationId)
		{
			var p = new DynamicParameters();
			p.Add("p_ReservationId", reservationId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_RESERVATION.DeleteReservation", p, commandType: CommandType.StoredProcedure);
		}
	}
}
