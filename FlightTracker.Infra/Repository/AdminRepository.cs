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
	public class AdminRepository : IAdminRepository
	{
		private readonly IDbContext _dbContext;

		public AdminRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Admin> GetAllAdmins()
		{
			IEnumerable<Admin> result = _dbContext.Connection.Query<Admin>("PKG_ADMIN.GetAllAdmins", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public Admin? GetAdminById(int adminId)
		{
			var p = new DynamicParameters();
			p.Add("p_AdminId", adminId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Admin> result = _dbContext.Connection.Query<Admin>("PKG_ADMIN.GetAdminById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public void UpdateAdmin(Admin admin)
		{
			var p = new DynamicParameters();
			p.Add("p_AdminId", admin.Adminid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FirstName", admin.Firstname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_LastName", admin.Lastname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_PhoneNumber", admin.Phonenumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", admin.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath", admin.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_ADMIN.UpdateAdmin", p, commandType: CommandType.StoredProcedure);
		}
	}
}
