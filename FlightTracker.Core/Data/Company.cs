using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Company
    {
        public Company()
        {
            Flights = new HashSet<Flight>();
        }

        public decimal Companyid { get; set; }
        public string? Imagepath { get; set; }
        public string Companyname { get; set; } = null!;

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
