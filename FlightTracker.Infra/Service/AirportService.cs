using FlightTracker.API.Requests.Airport;
using FlightTracker.Core.Data;
using FlightTracker.Core.Repository;
using FlightTracker.Core.Service;


namespace FlightTracker.Infra.Service
{
	public class AirportService:IAirportService
	{

		private readonly IAirportRepository airportRepository;


		public AirportService(IAirportRepository airportRepository)
		{
			this.airportRepository = airportRepository;
		}






		public void CreateAirport(CreateAirportRequest airport)
		{
			var myAirport = new Airport()
			{
				City = airport.City,
				Country = airport.Country,
				Latitude = airport.Latitude,
				Longitude = airport.Longitude,
				Status = airport.Status,
				Name = airport.Name,
			};
			airportRepository.CreateAirport(myAirport);
		}
		public Airport? GetAirportById(int airportId)
		{
			return airportRepository.GetAirportById(airportId);
		}
		public List<Airport> GetAllAirports()
		{
			return airportRepository.GetAllAirports();
		}
		public bool UpdateAirport(int AirportId,UpdateAirportRequest airport)
		{
			var oldAirprt = airportRepository.GetAirportById(AirportId);
			if(oldAirprt == null)
				return false;

			oldAirprt.Name = airport.Name?? oldAirprt.Name;
			oldAirprt.City = airport.City ?? oldAirprt.City;
			oldAirprt.Country = airport.Country ?? oldAirprt.Country;
			oldAirprt.Status= airport.Status ?? oldAirprt.Status;
			oldAirprt.Latitude = airport.Latitude ?? oldAirprt.Latitude;
			oldAirprt.Longitude = airport.Longitude ?? oldAirprt.Longitude;


            airportRepository.UpdateAirport(oldAirprt);
			return true;
		}
		public void DeleteAirport(int airportId)
		{
			airportRepository.DeleteAirport(airportId);
		}






	}
}
