using FlightTracker.Core.Data;
using FlightTracker.Core.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Service
{
	public interface IUserService
	{
		User GetMyProfile();
		bool UpdateMyProfile(UpdateUserRequest request);
		List<User> GetAllUsers();
		List<User> GetUsersByName(string search);
		User? GetUserById(int id);
		void CreateTestMonial(string testmonialText);
        List<Testimonial> GetTestimonials();


    }
}
