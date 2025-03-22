using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface IUserLoginRepository
	{
		List<Userlogin> GetAllLogins();
		Userlogin? GetLoginById(int loginId);
		void CreateLogin(Userlogin userLogin);
		void UpdateLogin(Userlogin userLogin);
		void DeleteLogin(int loginId);
		Userlogin? GetLoginByUsername(string username);
		Userlogin? AuthenticateUser(string username, string password);
	}
}
