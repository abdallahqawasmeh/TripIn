using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.ManagePages.ContactUs
{
	public class ContactUsRequest
	{
		public string Fullname { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Subject { get; set; } = null!;
		public string Message { get; set; } = null!;


	}
}
