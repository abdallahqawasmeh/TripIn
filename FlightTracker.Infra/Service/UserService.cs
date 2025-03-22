using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Requests.User;
using FlightTracker.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly IClaimsReader _claimsReader;

		public UserService(IUserRepository userRepository, IUserLoginRepository userLoginRepository,IClaimsReader claimsReader)
		{
			_userRepository = userRepository;
			_userLoginRepository = userLoginRepository;
			_claimsReader = claimsReader;
		}

		public User GetMyProfile()
		{
			var usernamme = _claimsReader.GetByClaimType(ClaimTypes.Name)!; 
			var userLogin = _userLoginRepository.GetLoginByUsername(usernamme)!;
			return  _userRepository.GetUserById((int)userLogin.User_Id!)!;
		}
		public bool UpdateMyProfile(UpdateUserRequest request)
		{
            var usernamme = _claimsReader.GetByClaimType(ClaimTypes.Name)!;
            var userLogin = _userLoginRepository.GetLoginByUsername(usernamme)!;
            var user =  _userRepository.GetUserById((int)userLogin.User_Id!)!;
			user.Email = request.Email ?? user.Email;
			user.Firstname = request.Firstname ?? user.Firstname;
			user.Imagepath = request.Imagepath ?? user.Imagepath;
			user.Lastname = request.Lastname ?? user.Lastname;
			user.Phonenumber = request.Phonenumber ?? user.Phonenumber;


			if(request.Username != null && userLogin.Username != request.Username )
			{
				var newUsernameLogin = _userLoginRepository.GetLoginByUsername(request.Username);
				if (newUsernameLogin != null)
					return false;

                userLogin.Username = request.Username ?? userLogin.Username;
            }
            userLogin.Password = request.Password ?? userLogin.Password;

			_userLoginRepository.UpdateLogin(userLogin);
			_userRepository.UpdateUser(user);
			return true;
        }


		public List<User> GetAllUsers()
		{
			return _userRepository.GetAllUsers();
		}

		public List<User> GetUsersByName(string search)
		{
			return GetAllUsers().Where(x => x.Firstname.Contains(search)).ToList();
		}

		public User? GetUserById (int id)
		{
			return _userRepository.GetUserById(id);
		}	
	}
}
