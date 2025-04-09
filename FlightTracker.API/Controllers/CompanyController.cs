using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.Company;
using FlightTracker.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{



		private readonly ICompanyService _companyService;

		public CompanyController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		[HttpPost]
		public IActionResult CreateCompany([FromBody] CreateCompanyRequest companyRequest)
		{
			if (companyRequest == null)
				return BadRequest("Company data is invalid.");

			_companyService.CreateCompany(companyRequest);
			return NoContent();
		}

		[HttpGet("{companyId}")]
		public IActionResult GetCompanyById(int companyId)
		{
			var company = _companyService.GetCompanyById(companyId);
			if (company == null)
				return NotFound();

			return Ok(company);
		}

		[HttpGet]
		public IActionResult GetAllCompanies()
		{
			var companies = _companyService.GetAllCompanies();
			return Ok(companies);
		}

		[HttpPut("{companyId}")]
		public IActionResult UpdateCompany(int companyId, [FromBody] UpdateCompanyRequest companyRequest)
		{
			if (companyRequest == null)
				return BadRequest("Invalid company data.");

			var result = _companyService.UpdateCompany(companyId, companyRequest);
			if (!result)
				return NotFound();

			return NoContent();
		}

		[HttpDelete("{companyId}")]
		public IActionResult DeleteCompany(int companyId)
		{
			var company = _companyService.GetCompanyById(companyId);
			if (company == null)
				return NotFound();

			_companyService.DeleteCompany(companyId);
			return NoContent();
		}









		//[HttpPost]
		//[Route("UploadImage")]

		//public Company UploadImage()
		//{
		//	var file = Request.Form.Files[0];
		//	var filename = Guid.NewGuid().ToString() + "_" + file.FileName;
		//	var fullpath= Path.Combine("Images",filename);
		//	using(var fileStream = new FileStream(fullpath, FileMode.Create))
		//	{
		//		file.CopyTo(fileStream);
		//	}

		//	Company item =new Company();
		//	item.Imagepath = filename;
		//	return item;


		//}
	}
}
