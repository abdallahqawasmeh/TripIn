using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static FlightTracker.Infra.Service.ClaimsReader;

namespace FlightTracker.Infra.Service
{

	[Authorize]
	public class ClaimsReader : IClaimsReader
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClaimsReader(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string? GetByClaimType(string claimType)
		{
            return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
		}
	}
}
