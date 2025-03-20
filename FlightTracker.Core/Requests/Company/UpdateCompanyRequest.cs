using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Company
{
	public class UpdateCompanyRequest
	{
        public string? Imagepath { get; set; }
        public string? Companyname { get; set; } 
    }
}
