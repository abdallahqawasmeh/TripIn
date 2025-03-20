using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Flight
{
	public class CreateFlightRequest
	{
        public string FlightName { get; set; } = null!;
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public decimal Price { get; set; }
        public decimal Availableseats { get; set; }
        public decimal? Status { get; set; } = 1;
        public decimal? Arrivalairportid { get; set; }
        public decimal? Departureairportid { get; set; }
        public decimal Companyid { get; set; }


    }
}
