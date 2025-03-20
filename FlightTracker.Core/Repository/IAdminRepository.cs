using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IAdminRepository
	{

		List<Admin> GetAllAdmins();
		Admin? GetAdminById(int adminId);
		void UpdateAdmin(Admin admin);

	}
}
