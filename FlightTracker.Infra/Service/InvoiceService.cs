using FlightTracker.Core.Data;

using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout;

namespace FlightTracker.Infra.Service
{
	public class InvoiceService
	{


		public string GenerateInvoice(Invoice invoice)
		{
			var invoiceFolder = Path.Combine("Invoices");
			Directory.CreateDirectory(invoiceFolder);

			var invoiceFileName = $"Invoice_{Guid.NewGuid()}{invoice.Userid}.pdf";
			var invoicePath = Path.Combine(invoiceFolder, invoiceFileName);

			using (var writer = new PdfWriter(invoicePath))
			using (var pdf = new PdfDocument(writer))
			using (var document = new Document(pdf))
			{
				document.SetMargins(40, 40, 40, 40);

				document.Add(new Paragraph("Flight Tracker Invoice")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(24)
					.SetBold());

				document.Add(new Paragraph("Invoice")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20)
					.SetMarginBottom(30));

				var table = new Table(2)
					.SetWidth(UnitValue.CreatePercentValue(100))
					.SetMarginBottom(20);

				AddTableRow(table, "Invoice Number:", Guid.NewGuid().ToString());
				AddTableRow(table, "Invoice Date:", invoice.Invoicedate?.ToString("dd/MM/yyyy") ?? "-");
				AddTableRow(table, "User:", $"{invoice.User?.Firstname} {invoice.User?.Lastname}");
				AddTableRow(table, "Flight Number:", invoice.Flight?.Flightnumber ?? "-");
				AddTableRow(table, "Passengers (Tax):", invoice.Tax?.ToString() ?? "0");
				AddTableRow(table, "Discount:", $"{invoice.Discount?.ToString("F2") ?? "0.00"} JOD");
				AddTableRow(table, "Total Amount:", $"{invoice.Totalamount?.ToString("F2") ?? "0.00"} JOD");


				document.Add(table);

				document.Add(new Paragraph("Terms and Conditions:")
					.SetBold()
					.SetMarginTop(20));

				document.Add(new Paragraph(
					"1. This invoice confirms your flight booking.\n" +
					"2. Booking is subject to airline terms and conditions.\n" +
					"3. Retain this invoice for your records.")
					.SetFontSize(10));

				document.Add(new Paragraph("Thank you for choosing Flight Tracker!")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetMarginTop(30)
					.SetItalic());
			}

			return $"/Invoices/{invoiceFileName}";
		}

		private void AddTableRow(Table table, string label, string value)
		{
			table.AddCell(new Cell()
				.Add(new Paragraph(label))
				.SetBold()
				.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
				.SetPadding(5));

			table.AddCell(new Cell()
				.Add(new Paragraph(value))
				.SetPadding(5));
		}
	}
}
