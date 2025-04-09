using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.ManagePages.AboutUs;
using FlightTracker.Core.Requests.ManagePages.ContactInfo;
using FlightTracker.Core.Requests.ManagePages.Home;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
	[ApiController]
	public class ManagePagesController : ControllerBase
	{



		private readonly IManagePages _managePagesService;

		public ManagePagesController(IManagePages managePagesService)
		{
			_managePagesService = managePagesService;
		}

		[HttpGet("AboutUs/{id}")]
		public IActionResult GetAboutUs(int id)
		{
			var aboutUs = _managePagesService.GetAboutUsById(id);
			if (aboutUs == null)
				return NotFound();

			return Ok(aboutUs);
		}

		[HttpPut("AboutUs/{id}")]
		public IActionResult UpdateAboutUs(int id, [FromBody] UpdateAboutUsRequest request)
		{
			var result = _managePagesService.UpdateAboutUs(id, request);
			if (!result)
				return NotFound();

			return Ok(new { Message = "About Us updated successfully." });
		}

		[HttpGet("ContactInfo/{id}")]
		public IActionResult GetContactInfo(int id)
		{
			var contactInfo = _managePagesService.GetContactInfoById(id);
			if (contactInfo == null)
				return NotFound();

			return Ok(contactInfo);
		}

		[HttpPut("ContactInfo/{id}")]
		public IActionResult UpdateContactInfo(int id, [FromBody] UpdateContactInfoRequest request)
		{
			var result = _managePagesService.UpdateContactInfo(id, request);
			if (!result)
				return NotFound();

			return Ok(new { Message = "Contact Info updated successfully." });
		}

		[HttpGet("Home/{id}")]
		public IActionResult GetHome(int id)
		{
			var home = _managePagesService.GetHomeById(id);
			if (home == null)
				return NotFound();

			return Ok(home);
		}

		[HttpPut("Home/{id}")]
		public IActionResult UpdateHome(int id, [FromBody] UpdateHomeRequest request)
		{
			var result = _managePagesService.UpdateHome(id, request);
			if (!result)
				return NotFound();

			return Ok(new { Message = "Home updated successfully." });
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("UploadImage")]
		public IActionResult UploadImage()
		{
			var file = Request.Form.Files[0];
			var filename = Guid.NewGuid().ToString() + "_" + file.FileName;
			var directoryPath = Path.Combine("Images");

			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);

			var fullpath = Path.Combine("Images", filename);

			using (var fileStream = new FileStream(fullpath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}

			return Ok(new { filepath = $"{filename}" });
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("GetImage/{filename}")]
		public IActionResult GetImage(string filename)
		{
			var imagePath = Path.Combine("Images", filename);

			if (!System.IO.File.Exists(imagePath))
				return NotFound();

			var imageFileStream = System.IO.File.OpenRead(imagePath);
			var mimeType = "image/" + Path.GetExtension(filename).Trim('.').ToLower();
			return File(imageFileStream, mimeType);





		}










	}
}
