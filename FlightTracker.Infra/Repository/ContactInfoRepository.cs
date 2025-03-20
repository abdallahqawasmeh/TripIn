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
	public class ContactInfoRepository : IContactInfoRepository
	{
		private readonly IDbContext _dbContext;

		public ContactInfoRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateContactInfo(Contactinfo contactInfo)
		{
			var p = new DynamicParameters();
			p.Add("p_PhoneNumber", contactInfo.Phonenumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", contactInfo.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_FacebookLink", contactInfo.Facebooklink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_InstagramLink", contactInfo.Instagramlink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_XLink", contactInfo.Xlink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Copyright", contactInfo.Copyright, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Location", contactInfo.Location, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactInfo.CreateContactInfo", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateContactInfo(Contactinfo contactInfo)
		{
			var p = new DynamicParameters();
			p.Add("p_ContactId", contactInfo.Contactid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_PhoneNumber", contactInfo.Phonenumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", contactInfo.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_FacebookLink", contactInfo.Facebooklink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_InstagramLink", contactInfo.Instagramlink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_XLink", contactInfo.Xlink, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Copyright", contactInfo.Copyright, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Location", contactInfo.Location, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactInfo.UpdateContactInfo", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteContactInfo(int contactId)
		{
			var p = new DynamicParameters();
			p.Add("p_ContactId", contactId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ContactInfo.DeleteContactInfo", p, commandType: CommandType.StoredProcedure);
		}
	}
}
