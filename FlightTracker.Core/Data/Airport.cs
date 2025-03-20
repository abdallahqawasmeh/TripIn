using System;
using System.Collections.Generic;

namespace FlightTracker.Core.Data
{
    public partial class Airport
    {
        public Airport()
        {
            FlightArrivalairports = new HashSet<Flight>();
            FlightDepartureairports = new HashSet<Flight>();
        }

        public decimal Airportid { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public decimal? Status { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public virtual ICollection<Flight> FlightArrivalairports { get; set; }
        public virtual ICollection<Flight> FlightDepartureairports { get; set; }
    }
}
