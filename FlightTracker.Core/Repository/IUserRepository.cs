using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IUserRepository
	{





		List<User> GetAllUsers();
		User? GetUserById(int userId);
		void CreateUser(User user, Userlogin userlogin);
		void UpdateUser(User user);
		void DeleteUser(int userId);



	}
}
