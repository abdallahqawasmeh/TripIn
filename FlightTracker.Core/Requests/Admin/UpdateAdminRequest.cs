using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Admin
{
    public class UpdateAdminRequest
    {

		public string? Firstname { get; set; } = null!;
		public string? Lastname { get; set; } = null!;
		public string? Phonenumber { get; set; }
		public string? Email { get; set; }
		public string? Imagepath { get; set; }
		public string? Password { get; set; }
		public string? userName { get; set; }
	}
}
