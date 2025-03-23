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
	public class PaymentRepository : IPaymentRepository
	{
		private readonly IDbContext _dbContext;

		public PaymentRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Payment? GetPaymentById(int paymentId)
		{
			var p = new DynamicParameters();
			p.Add("p_PaymentId", paymentId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Payment> result = _dbContext.Connection.Query<Payment>(
				"PKG_PAYMENT.GetPaymentById",
				p,
				commandType: CommandType.StoredProcedure
			);
			return result.FirstOrDefault();
		}

		public void UpdateFlightId(int paymentId, int flightId)
		{
			var p = new DynamicParameters();
			p.Add("p_PaymentId", paymentId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FlightId", flightId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_PAYMENT.UpdateFlightId", p, commandType: CommandType.StoredProcedure);
		}




		public void UpdatePaymentBalance(decimal balance)
		{
			var p = new DynamicParameters();
			p.Add("p_Balance", balance, dbType: DbType.Decimal, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_PAYMENT.UpdatePaymentBalance", p, commandType: CommandType.StoredProcedure);
		}

	}
}
