using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Requests.Auth
{
    public class SignUpUserRequest
    {

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Imagepath { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
