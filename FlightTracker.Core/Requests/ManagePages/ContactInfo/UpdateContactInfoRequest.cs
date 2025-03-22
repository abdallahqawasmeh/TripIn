using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.ManagePages.ContactInfo
{
	public class UpdateContactInfoRequest
	{

		public string? Phonenumber { get; set; } 
		public string? Email { get; set; } = null!;
		public string? Facebooklink { get; set; }
		public string? Instagramlink { get; set; }
		public string? Xlink { get; set; }
		public string? Copyright { get; set; }
		public string? Location { get; set; } 
	}
}
