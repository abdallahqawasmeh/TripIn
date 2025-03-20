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
	public class RoleRepository : IRoleRepository
	{
		private readonly IDbContext _dbContext;

		public RoleRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Role? GetRoleById(int roleId)
		{
			var p = new DynamicParameters();
			p.Add("p_RoleId", roleId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Role> result = _dbContext.Connection.Query<Role>(
				"PKG_Roles.GetRoleById",
				p,
				commandType: CommandType.StoredProcedure
			);
			return result.FirstOrDefault();
		}

		public void UpdateRole(Role role)
		{
			var p = new DynamicParameters();
			p.Add("p_RoleId", role.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_RoleName", role.Rolename, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_Roles.UpdateRole", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteRole(int roleId)
		{
			var p = new DynamicParameters();
			p.Add("p_RoleId", roleId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_Roles.DeleteRole", p, commandType: CommandType.StoredProcedure);
		}
	}
}
