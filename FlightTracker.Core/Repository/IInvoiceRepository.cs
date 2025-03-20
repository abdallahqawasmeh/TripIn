using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IInvoiceRepository
	{

		List<Invoice> GetAllInvoices();
		Invoice? GetInvoiceById(int invoiceId);
		List<Invoice> GetInvoicesByUser(int userId);
		void CreateInvoice(Invoice invoice);
		void UpdateInvoice(Invoice invoice);
		void DeleteInvoice(int invoiceId);

	}
}
