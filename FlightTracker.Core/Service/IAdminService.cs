using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IAdminService
	{
		bool UpdateAdminProfile(UpdateAdminRequest request);
		void UpdateTestmonialStatus(int status, int testmonialId);
		List<Testimonial> GetNewTestimonials();
		Admin GetMyProfile();
		List<User> GetTop10();
		Report Reports(DateOnly? Start, DateOnly? End);
		string GenerateReport(DateOnly? start, DateOnly? end);



	}
}
