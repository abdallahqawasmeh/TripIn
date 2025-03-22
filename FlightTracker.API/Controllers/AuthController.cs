using FlightTracker.Core.Requests.Auth;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("sign-up")]
		public IActionResult SingUp(SignUpUserRequest request)
		{
			var result = _authService.SingUp(request);
			if (result == null)
				return BadRequest("username already exists");

            return Ok(new { jwt = result });

        }

        [HttpPost("login")]
        public IActionResult Login(string Username,string Password)
        {
			var result = _authService.Login(Username, Password);
			if(result == null)
			{
				return BadRequest("username or password are incorrect");
			}
			return Ok(new { jwt = result });
        }


    }
}
