using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.Company;
using FlightTracker.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Service
{
	public class CompanyService :ICompanyService
	{

		private readonly ICompanyRepository companyRepository;

		public CompanyService(ICompanyRepository companyRepository)
		{
			this.companyRepository = companyRepository;
		}

		public void CreateCompany(CreateCompanyRequest company)
		{

			var comp = new Company()
			{
				Companyname = company.Companyname,
				Imagepath = company.Imagepath,
			};


			companyRepository.CreateCompany(comp);	
		}

		public void DeleteCompany(int companyId)
		{
			companyRepository.DeleteCompany(companyId);
		}

		public List<Company> GetAllCompanies()
		{
			return companyRepository.GetAllCompanies();	
		}

		public Company? GetCompanyById(int companyId)
		{
			return companyRepository.GetCompanyById(companyId);	
		}

		public bool UpdateCompany(int CompId,UpdateCompanyRequest company)
		{
			var oldcomp = companyRepository.GetCompanyById(CompId);

			if (oldcomp != null)
			{

				oldcomp.Companyname = company.Companyname ?? oldcomp.Companyname;
				oldcomp.Imagepath = company.Imagepath ?? oldcomp.Imagepath;
				companyRepository.UpdateCompany(oldcomp);
				return true;
			}
			return false;


		}

    }
}
