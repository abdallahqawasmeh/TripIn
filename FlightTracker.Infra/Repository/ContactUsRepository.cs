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



	public class ContactUsRepository : IContactUsRepository
	{
		private readonly IDbContext _dbContext;

		public ContactUsRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateContactUs(Contactu contactUs)
		{
			var p = new DynamicParameters();
			p.Add("p_FullName", contactUs.Fullname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", contactUs.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Subject", contactUs.Subject, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Message", contactUs.Message, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_SentDate", contactUs.Sentdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("p_UserId", contactUs.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactUs.CreateContactUs", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateContactUs(Contactu contactUs)
		{
			var p = new DynamicParameters();
			p.Add("p_ContactUsId", contactUs.Contactusid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FullName", contactUs.Fullname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", contactUs.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Subject", contactUs.Subject, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Message", contactUs.Message, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_SentDate", contactUs.Sentdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			p.Add("p_UserId", contactUs.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactUs.U	pdateContactUs", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteContactUs(int contactUsId)
		{
			var p = new DynamicParameters();
			p.Add("p_ContactUsId", contactUsId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactUs.DeleteContactUs", p, commandType: CommandType.StoredProcedure);
		}
	}
	
}
