using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.ManagePages.ContactUs;
using FlightTracker.Core.Requests.User;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		// GET: api/user/profile
		[Authorize(Roles ="user")]
		[HttpGet("profile")]
		public ActionResult<User> GetMyProfile()
		{
			
			var user = _userService.GetMyProfile();
			return Ok(user);
		}

        // PUT: api/user/profile
        [Authorize(Roles = "user")]

        [HttpPut("profile")]
		public IActionResult UpdateMyProfile([FromBody] UpdateUserRequest request)
		{
			var result = _userService.UpdateMyProfile(request);
			if (!result)
				return Conflict("Username already exists.");
			return NoContent();
		}

        // GET: api/user
        [Authorize(Roles = "admin")]
        [HttpGet]
		public ActionResult<List<User>> GetAllUsers()
		{
			var users = _userService.GetAllUsers();
			return Ok(users);
		}

        // GET: api/user/search?name=John
        [Authorize(Roles = "admin")]

        [HttpGet("search")]
		public ActionResult<List<User>> GetUsersByName([FromQuery] string name)
		{
			var users = _userService.GetUsersByName(name);
			return Ok(users);
		}

        // GET: api/user/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
		public ActionResult<User> GetUserById(int id)
		{
			var user = _userService.GetUserById(id);
			if (user == null)
				return NotFound();
			return Ok(user);
		}
		[HttpGet("testimonial")]
		public IActionResult GetTestiMonials()
		{
			return Ok(_userService.GetTestimonials());
		}
        [HttpPost("testimonial")]
        public IActionResult CreateTestimonial(string text)
		{
			_userService.CreateTestMonial(text);
			return Ok();
		}

		[HttpPost("ContactUs")]
		public IActionResult SendEmailContactUs(ContactUsRequest request)
		{
		   _userService.SendContactUsEmailAsync(request);
			return NoContent();



		}








	}
}

