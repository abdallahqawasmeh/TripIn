using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface ICompanyRepository
	{
		void CreateCompany(Company company);
		Company? GetCompanyById(int companyId);
		List<Company> GetAllCompanies();
		void UpdateCompany(Company company);
		void DeleteCompany(int companyId);
	}
}
