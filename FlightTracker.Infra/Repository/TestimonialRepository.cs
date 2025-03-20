using Dapper;
using FlightTracker.Core.Common;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Infra.Repository
{
	public class TestimonialRepository : ITestimonialRepository
	{
		private readonly IDbContext _dbContext;

		public TestimonialRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Testimonial> GetAllTestimonials()
		{
			IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("PKG_TESTIMONIAL.GetAllTestimonials", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public Testimonial? GetTestimonialById(int testimonialId)
		{
			var p = new DynamicParameters();
			p.Add("p_TestimonialId", testimonialId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("PKG_TESTIMONIAL.GetTestimonialById", p, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}

		public List<Testimonial> GetTestimonialsForAdmin()
		{
			IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("PKG_TESTIMONIAL.GetTestimonialsForAdmin", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public List<Testimonial> GetTestimonialsForFront()
		{
			IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("PKG_TESTIMONIAL.GetTestimonialsForFront", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public void CreateTestimonial(Testimonial testimonial)
		{
			var p = new DynamicParameters();
			p.Add("p_UserId", testimonial.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_TestimonialText", testimonial.Testimonialtext, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_TESTIMONIAL.CreateTestimonial", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateTestimonialStatus(int testimonialId, int status)
		{
			var p = new DynamicParameters();
			p.Add("p_TestimonialId", testimonialId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_Status", status, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_TESTIMONIAL.UpdateTestimonialStatus", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteTestimonial(int testimonialId)
		{
			var p = new DynamicParameters();
			p.Add("p_TestimonialId", testimonialId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_TESTIMONIAL.DeleteTestimonial", p, commandType: CommandType.StoredProcedure);
		}
	}
}
