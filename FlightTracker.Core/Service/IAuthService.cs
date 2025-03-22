using FlightTracker.Core.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IAuthService
	{

		string? SingUp(SignUpUserRequest request);

		string? Login(string Username, string Password);

		string AuthUser(string username);
		string AuthAdmin(string username);
	}
}
