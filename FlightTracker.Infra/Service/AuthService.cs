using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FlightTracker.Core.Requests.Auth;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Data;
using FlightTracker.Core.Service;

namespace FlightTracker.Infra.Service
{
	public class AuthService: IAuthService
	{
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginRepository _userLoginRepository;

		public AuthService(IUserRepository userRepository, IUserLoginRepository userLoginRepository)
		{
			_userRepository = userRepository;
			_userLoginRepository = userLoginRepository;
		}

		public string? SingUp(SignUpUserRequest request)
        {
            var user = _userLoginRepository.GetLoginByUsername(request.Username);
            if(user != null)
            {
                return null;
            }

            var newUser = new User() 
            { 
            Firstname=request.Firstname,
            Lastname =request.Lastname,
            Email=request.Email,
            Phonenumber=request.Phonenumber,
            Imagepath  =request.Imagepath,

            };
            var newUserLogin = new Userlogin() 
            {
                Username =request.Username,
                Password =request.Password
            
            };
            _userRepository.CreateUser(newUser, newUserLogin);
            return AuthUser(request.Username);
        }
        public string? Login(string Username,string Password)
        {
            var user = _userLoginRepository.GetLoginByUsername(Username);
            if (user == null)
            {
                return null;
            }
            user = _userLoginRepository.GetLoginById((int)user.Loginid)!;
            if(Password == user.Password)
            {
                if(user.Role_id == 2)
                    return AuthUser(Username);
                return AuthAdmin(Username);
            }
            return null;
               

        }
        public string AuthUser(string username)
        {

            // read about refresh token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"));
            var signCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                      {
                          new Claim(ClaimTypes.Name,username),
                          new Claim(ClaimTypes.Role,"user"),
                      };

            var tokenOption = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signCredential
                );


            return new JwtSecurityTokenHandler().WriteToken(tokenOption);

        }
       
		public string AuthAdmin(string username)
        {


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"));
            var signCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"admin")

                };

            var tokenOption = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signCredential
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOption);

        }






    }
}
