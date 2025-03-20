using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Bookflight
    {
        public decimal Bookid { get; set; }
        public decimal Allnumberofpassengers { get; set; }//total 
        public decimal? Flightid { get; set; }

        public virtual Flight? Flight { get; set; }
    }
}
