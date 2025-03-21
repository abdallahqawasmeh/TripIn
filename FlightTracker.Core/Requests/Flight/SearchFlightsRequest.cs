using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Flight
{
    public class SearchFlightsRequest
    {
		public string ?Start { get; set; }
		public string? End { get; set; }
		public int ArrivalAirPortId { get; set; }
		public int DepartureAirportId { get; set; }
		public bool SortByFastest { get; set; } = false;
		public bool Des { get; set; } = false;

        public DateOnly ?StartDateOnly => Start == null ? null : DateOnly.Parse(Start);
		public DateOnly ?EndDateOnly => End==null ? null: DateOnly.Parse(End);
	}
}
