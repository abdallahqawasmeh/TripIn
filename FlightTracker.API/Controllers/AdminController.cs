using FlightTracker.Core.Requests.Admin;
using FlightTracker.Core.Service;
using FlightTracker.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{



		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet("profile")]
		public IActionResult GetMyProfile()
		{
			return Ok(_adminService.GetMyProfile());
		}

		
		[HttpPut("UpdateProfile")]
		public IActionResult UpdateAdminProfile([FromBody] UpdateAdminRequest request)
		{
			var result = _adminService.UpdateAdminProfile(request);
			if (!result)
				return BadRequest("Update Failed. Username might already exist or admin not found.");

			return Ok(new { message = "Profile Updated Successfully" });
		}

		[HttpPut("testimonial/{testimonialId}/{status}")]
		public IActionResult UpdateTestimonialStatus(int testimonialId, int status)
		{
			_adminService.UpdateTestmonialStatus(status, testimonialId);
			return Ok("Testimonial Status Updated Successfully");
		}


		[HttpGet("testimonial")]
		public IActionResult GetNewTestimonials()
		{
			var testimonials = _adminService.GetNewTestimonials();
			return Ok(testimonials);
		}

		[HttpGet("top-users")]
		public IActionResult GetTopUsers()
		{
			return Ok(_adminService.GetTop10());
		}

		[HttpPatch("report")]
		public IActionResult GetReport(getReport reqeust)
		{
			return Ok(_adminService.Reports(reqeust.StartDateOnly, reqeust.EndDateOnly));                        


		}


        [HttpPatch("generate-report")]
        public IActionResult GenerateReport(getReport reqeust)
        {


			var path = _adminService.GenerateReport(reqeust.StartDateOnly, reqeust.EndDateOnly) ;
			var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), path.TrimStart('/'));

			if (!System.IO.File.Exists(absolutePath))
				return NotFound("Report file not found");

			return PhysicalFile(absolutePath, "application/pdf", Path.GetFileName(absolutePath));



        }







	}
}
