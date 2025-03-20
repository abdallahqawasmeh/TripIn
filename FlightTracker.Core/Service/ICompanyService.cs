using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface ICompanyService
	{


		void CreateCompany(CreateCompanyRequest company);
		Company? GetCompanyById(int companyId);
		List<Company> GetAllCompanies();
		bool UpdateCompany(int CompId,UpdateCompanyRequest company);
		void DeleteCompany(int companyId);

	}
}
