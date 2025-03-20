using FlightTracker.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Core.Repository
{
	public interface ITestimonialRepository
	{


		List<Testimonial> GetAllTestimonials();
		Testimonial? GetTestimonialById(int testimonialId);
		List<Testimonial> GetTestimonialsForAdmin();
		List<Testimonial> GetTestimonialsForFront();
		void CreateTestimonial(Testimonial testimonial);
		void UpdateTestimonialStatus(int testimonialId, int status);
		void DeleteTestimonial(int testimonialId);
	}
}
