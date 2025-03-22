using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.ManagePages.Home
{
	public class UpdateHomeRequest
	{

		public string? Companyname { get; set; }  
		public string? Imagepath1 { get; set; }
		public string? Imagepath2 { get; set; }
		public string? Imagepath3 { get; set; }
		public string? Paragraphbig { get; set; }
		public string? Paragraphsmall { get; set; }
		public string? Traveltext { get; set; }
		public string? Experiencetext { get; set; }
		public string? Additionaltext { get; set; }
		public string? Footerparagraph { get; set; }



	}
}
