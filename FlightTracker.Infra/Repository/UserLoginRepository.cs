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
	public class UserLoginRepository : IUserLoginRepository
	{
		private readonly IDbContext _dbContext;
		public UserLoginRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Userlogin> GetAllLogins()
		{
			IEnumerable<Userlogin> result = _dbContext.Connection.Query<Userlogin>(
				"UserLogin_Package.GetAllLogins",
				commandType: CommandType.StoredProcedure
			);
			return result.ToList();
		}

		public Userlogin? GetLoginById(int loginId)
		{
			var p = new DynamicParameters();
			p.Add("ID", loginId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Userlogin> result = _dbContext.Connection.Query<Userlogin>(
				"UserLogin_Package.GetLoginById",
			p,
				commandType: CommandType.StoredProcedure
			);
			return result.FirstOrDefault();
		}
		public Userlogin? GetLoginByUsername(string username)
{
	var p = new DynamicParameters();
	p.Add("p_Username", username, DbType.String, ParameterDirection.Input);

	IEnumerable<Userlogin> result = _dbContext.Connection.Query<Userlogin>(
		"UserLogin_Package.GetLoginByUsername",
		p,
		commandType: CommandType.StoredProcedure
	);

	return result.FirstOrDefault();
}

		public void CreateLogin(Userlogin userLogin)
		{
			var p = new DynamicParameters();
			p.Add("username", userLogin.Username, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("password", userLogin.Password, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("role_id", userLogin.Role_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("user_id", userLogin.User_Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
		

			_dbContext.Connection.Execute("UserLogin_Package.CreateLogin", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateLogin(Userlogin userLogin)
		{
			var p = new DynamicParameters();
			p.Add("ID", userLogin.Loginid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("username", userLogin.Username, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("password", userLogin.Password, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("role_id", userLogin.Role_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("user_id", userLogin.User_Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("UserLogin_Package.UpdateLogin", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteLogin(int loginId)
		{
			var p = new DynamicParameters();
			p.Add("ID", loginId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("UserLogin_Package.DeleteLogin", p, commandType: CommandType.StoredProcedure);
		}

		public Userlogin? AuthenticateUser(string username, string password)
		{
			var p = new DynamicParameters();
			p.Add("user_name", username, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("pass", password, dbType: DbType.String, direction: ParameterDirection.Input);

			IEnumerable<Userlogin> result = _dbContext.Connection.Query<Userlogin>(
				"UserLogin_Package.User_Login",
				p,
				commandType: CommandType.StoredProcedure
			);
			return result.FirstOrDefault();
		}
	}
}
