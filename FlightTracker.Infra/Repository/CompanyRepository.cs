using Dapper;
using FlightTracker.Core.Common;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly IDbContext _dbContext;

		public CompanyRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateCompany(Company company)
		{
			var p = new DynamicParameters();
			p.Add("p_ImagePath", company.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_CompanyName", company.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_COMPANY.CreateCompany", p, commandType: CommandType.StoredProcedure);
		}

		public Company? GetCompanyById(int companyId)
		{
			var p = new DynamicParameters();
			p.Add("p_CompanyId", companyId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Company> result = _dbContext.Connection.Query<Company>("PKG_COMPANY.GetCompanyById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public List<Company> GetAllCompanies()
		{
			IEnumerable<Company> result = _dbContext.Connection.Query<Company>("PKG_COMPANY.GetAllCompanies", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public void UpdateCompany(Company company)
		{
			var p = new DynamicParameters();
			p.Add("p_CompanyId", company.Companyid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_ImagePath", company.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_CompanyName", company.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_COMPANY.UpdateCompany", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteCompany(int companyId)
		{
			var p = new DynamicParameters();
			p.Add("p_CompanyId", companyId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_COMPANY.DeleteCompany", p, commandType: CommandType.StoredProcedure);
		}
	}
}
