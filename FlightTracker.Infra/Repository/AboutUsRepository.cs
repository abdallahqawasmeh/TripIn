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
	public class AboutUsRepository : IAboutUsRepository
	{
		private readonly IDbContext _dbContext;

		public AboutUsRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateAboutUs(Aboutu aboutUs)
		{
			var p = new DynamicParameters();
			p.Add("p_AboutText", aboutUs.Aboutustext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Image1", aboutUs.Imagepath1, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Image2", aboutUs.Imagepath2, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_CompName", aboutUs.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AboutUs.CreateAboutUs", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateAboutUs(Aboutu aboutUs)
		{
			var p = new DynamicParameters();
			p.Add("p_Id", aboutUs.Aboutusid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_AboutText", aboutUs.Aboutustext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Image1", aboutUs.Imagepath1, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Image2", aboutUs.Imagepath2, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_CompName", aboutUs.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AboutUs.UpdateAboutUs", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteAboutUs(int id)
		{
			var p = new DynamicParameters();
			p.Add("p_Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_AboutUs.DeleteAboutUs", p, commandType: CommandType.StoredProcedure);
		}
	}
}
