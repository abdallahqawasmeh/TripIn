using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.User
{
	public class UpdateUserRequest
	{

		public string ?Firstname { get; set; } 
		public string ?Lastname { get; set; } 
		public string ?Phonenumber { get; set; } 
		public string ?Email { get; set; } 
		public string ?Imagepath { get; set; } 
		public string? Password { get; set; } 
		public string? Username { get; set; } 



	}
}
