using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Reservation
    {
        public decimal Reservationid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Flightid { get; set; }
        public DateTime? Reservationdate { get; set; }
        public decimal? Totalamount { get; set; }

        public virtual Flight? Flight { get; set; }
        public virtual User? User { get; set; }
    }
}
