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
	public class UserRepository : IUserRepository
	{
		private readonly IDbContext _dbContext;

		public UserRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<User> GetAllUsers()
		{
			IEnumerable<User> result = _dbContext.Connection.Query<User>("PKG_USERS.GetAllUsers", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public User? GetUserById(int userId)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<User> result = _dbContext.Connection.Query<User>("PKG_USERS.GetUserById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public void CreateUser(User user, Userlogin userlogin)
		{
			var p = new DynamicParameters();

			p.Add("p_FirstName", user.Firstname, DbType.String, ParameterDirection.Input);
			p.Add("p_LastName", user.Lastname, DbType.String, ParameterDirection.Input);
			p.Add("p_PhoneNumber", user.Phonenumber, DbType.String, ParameterDirection.Input);
			p.Add("p_Email", user.Email, DbType.String, ParameterDirection.Input);
			p.Add("p_Username", userlogin.Username, DbType.String, ParameterDirection.Input);
			p.Add("p_Password", userlogin.Password, DbType.String, ParameterDirection.Input);
			p.Add("p_ImagePath", user.Imagepath, DbType.String, ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_USERS.CreateUser",p,	commandType: CommandType.StoredProcedure);
		}
		public void UpdateUser(User user)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", user.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_FirstName", user.Firstname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_LastName", user.Lastname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_PhoneNumber", user.Phonenumber, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_Email", user.Email, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath", user.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_USERS.UpdateUser", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteUser(int userId)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_USERS.DeleteUser", p, commandType: CommandType.StoredProcedure);
		}


	}
}
