using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Invoice
    {
        public decimal Invoiceid { get; set; }
        public decimal? Paymentid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Flightid { get; set; }
        public DateTime? Invoicedate { get; set; }
        public string? Filepath { get; set; }
        public decimal? Status { get; set; }
        public decimal? Tax { get; set; }//number of passenger
        public decimal? Discount { get; set; }
        public decimal? Totalamount { get; set; }

        public virtual Flight? Flight { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual User? User { get; set; }
    }
}
