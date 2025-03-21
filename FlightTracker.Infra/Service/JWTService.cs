using FlightTracker.Core.Data;
using LearningHub.core.Data;
using LearningHub.core.Repository;
using LearningHub.core.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Services
{
	public class JWTService 
	{


		public JWTService()
		{
		
		}

		public string Auth(User user)
		{

			if (result == null)
			{
				return null;
			}
			else
			{
				var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeyDana@345"));
				var signCredential = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,result.Username),
					new Claim("RoleId",result.Roleid.ToString()),


				};

				var tokenOption = new JwtSecurityToken(claims:claims,
					expires: DateTime.Now.AddHours(24),
					signingCredentials:signCredential
					);

				var tokenAsString = new JwtSecurityTokenHandler().WriteToken(tokenOption);


				return tokenAsString;
			}



		}
	}
}
