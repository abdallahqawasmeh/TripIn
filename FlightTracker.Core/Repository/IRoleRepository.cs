﻿using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IRoleRepository
	{
		Role? GetRoleById(int roleId);
		void UpdateRole(Role role);
		void DeleteRole(int roleId);
	}
}
