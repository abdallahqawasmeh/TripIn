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
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly IDbContext _dbContext;

		public InvoiceRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Invoice> GetAllInvoices()
		{
			IEnumerable<Invoice> result = _dbContext.Connection.Query<Invoice>("PKG_INVOICE.GetAllInvoices", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public Invoice? GetInvoiceById(int invoiceId)
		{
			var p = new DynamicParameters();
			p.Add("p_InvoiceId", invoiceId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Invoice> result = _dbContext.Connection.Query<Invoice>("PKG_INVOICE.GetInvoiceById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public List<Invoice> GetInvoicesByUser(int userId)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Invoice> result = _dbContext.Connection.Query<Invoice>("PKG_INVOICE.GetInvoicesByUser", p, commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public void CreateInvoice(Invoice invoice)
		{
			var p = new DynamicParameters();
			p.Add("p_PaymentId", invoice.Paymentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_UserId", invoice.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FlightId", invoice.Flightid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FilePath", invoice.Filepath, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Tax", invoice.Tax, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("p_Discount", invoice.Discount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("p_TotalAmount", invoice.Totalamount, dbType: DbType.Decimal, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_INVOICE.CreateInvoice", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateInvoice(Invoice invoice)
		{
			var p = new DynamicParameters();
			p.Add("p_InvoiceId", invoice.Invoiceid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FilePath", invoice.Filepath, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Status", invoice.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_Tax", invoice.Tax, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("p_Discount", invoice.Discount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			p.Add("p_TotalAmount", invoice.Totalamount, dbType: DbType.Decimal, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_INVOICE.UpdateInvoice", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteInvoice(int invoiceId)
		{
			var p = new DynamicParameters();
			p.Add("p_InvoiceId", invoiceId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_INVOICE.DeleteInvoice", p, commandType: CommandType.StoredProcedure);
		}
	}
}
