using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Payment
    {
        public Payment()
        {
            Invoices = new HashSet<Invoice>();
        }

        public decimal Paymentid { get; set; }
        public DateTime? Paymentdate { get; set; }
        public decimal Amountpaid { get; set; }
        public decimal? Paymentstatus { get; set; }
        public string? Cardholdername { get; set; }
        public string? Cardnumber { get; set; }
        public decimal? Balance { get; set; }
        public string? Expirydate { get; set; }
        public string? Cvc { get; set; }
        public decimal? Flightid { get; set; }

        public virtual Flight? Flight { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
