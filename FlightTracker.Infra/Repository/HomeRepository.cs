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
	public class HomeRepository : IHomeRepository
	{
		private readonly IDbContext _dbContext;

		public HomeRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void CreateHome(Home home)
		{
			var p = new DynamicParameters();
			p.Add("p_CompanyName", home.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath1", home.Imagepath1, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath2", home.Imagepath2, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath3", home.Imagepath3, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ParagraphBig", home.Paragraphbig, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ParagraphSmall", home.Paragraphsmall, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_TravelText", home.Traveltext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ExperienceText", home.Experiencetext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_AdditionalText", home.Additionaltext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_FooterParagraph", home.Footerparagraph, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_Home.CreateHome", p, commandType: CommandType.StoredProcedure);
		}

		public void UpdateHome(Home home)
		{
			var p = new DynamicParameters();
			p.Add("p_HomeId", home.Homeid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			p.Add("p_CompanyName", home.Companyname, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath1", home.Imagepath1, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath2", home.Imagepath2, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ImagePath3", home.Imagepath3, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ParagraphBig", home.Paragraphbig, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ParagraphSmall", home.Paragraphsmall, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_TravelText", home.Traveltext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_ExperienceText", home.Experiencetext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_AdditionalText", home.Additionaltext, dbType: DbType.String, direction: ParameterDirection.Input);
			p.Add("p_FooterParagraph", home.Footerparagraph, dbType: DbType.String, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_Home.UpdateHome", p, commandType: CommandType.StoredProcedure);
		}

		public void DeleteHome(int homeId)
		{
			var p = new DynamicParameters();
			p.Add("p_HomeId", homeId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			_dbContext.Connection.Execute("PKG_Home.DeleteHome", p, commandType: CommandType.StoredProcedure);
		}







		public Home? GetHomeById(int id)
		{
			var p = new DynamicParameters();
			p.Add("p_HomeId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			IEnumerable<Home> result = _dbContext.Connection.Query<Home>(
				"PKG_Home.GetHomeById",
				p,
				commandType: CommandType.StoredProcedure
			);

			return result.FirstOrDefault();
		}




	}
}

